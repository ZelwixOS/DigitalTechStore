import React from 'react';
import { GridColDef } from '@material-ui/data-grid';

import { TableBasement } from 'src/Components/Admin/Parts/TableBasement';
import { getAllRegions } from 'src/Requests/GetRequests';
import ModalFormDialog from 'src/Components/Admin/Parts/ModalFormDialog';
import { deleteRegion } from 'src/Requests/DeleteRequests';
import CreateRegion from 'src/Components/Admin/Parts/CreateRegion';
import EditRegion from 'src/Components/Admin/Parts/EditRegion';

export const RegionList = () => {
  const columns: GridColDef[] = [
    { field: 'id', headerName: 'ID', width: 100 },
    {
      field: 'name',
      headerName: 'Название',
      width: 350,
    },
  ];

  const onDelete = async (id: string): Promise<boolean> => {
    const res = await deleteRegion(parseInt(id));
    if (res === 0) {
      setError('Не удалось удалить объект. Возможно, существуют зависимости.');
      setOpen(true);
      return false;
    }

    return true;
  };

  const [open, setOpen] = React.useState(false);
  const [error, setError] = React.useState('');
  const [createOpen, setCreateOpen] = React.useState(false);
  const [editOpen, setEditOpen] = React.useState(false);
  const [refreshFunction, setRefrFun] = React.useState({ refresh: () => console.log('') });
  const [selected, setSelected] = React.useState('');

  const createNew = (refrFun: () => void) => {
    setCreateOpen(true);
    setRefrFun({ refresh: refrFun });
  };

  const editSelected = (selectedId: string, refrFun: () => void) => {
    setSelected(selectedId);
    setEditOpen(true);
    setRefrFun({ refresh: refrFun });
  };

  return (
    <React.Fragment>
      <TableBasement
        name="Регионы"
        getData={getAllRegions}
        columns={columns}
        pageSize={10}
        deleteSelected={onDelete}
        createNew={createNew}
        editSelected={editSelected}
        open={open}
        setOpen={setOpen}
        error={error}
      />
      <ModalFormDialog
        name={'Создание региона'}
        open={createOpen}
        form={<CreateRegion setOpen={setCreateOpen} refresher={refreshFunction} />}
        setOpen={setCreateOpen}
      />
      <ModalFormDialog
        name={'Изменение региона'}
        open={editOpen}
        form={<EditRegion id={selected} setOpen={setEditOpen} refresher={refreshFunction} />}
        setOpen={setEditOpen}
      />
    </React.Fragment>
  );
};
