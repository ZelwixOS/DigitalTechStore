import React from 'react';
import { GridColDef, GridValueGetterParams } from '@material-ui/data-grid';

import { TableBasement } from 'src/Components/Admin/Parts/TableBasement';
import { getAllOutlets } from 'src/Requests/GetRequests';
import ModalFormDialog from 'src/Components/Admin/Parts/ModalFormDialog';
import { deleteOutlet } from 'src/Requests/DeleteRequests';
import CreateOutlet from 'src/Components/Admin/Parts/CreateOutlet';
import EditOutlet from 'src/Components/Admin/Parts/EditOutlet';

export const OutletList = () => {
  const columns: GridColDef[] = [
    { field: 'id', headerName: 'ID', width: 100 },
    {
      field: 'name',
      headerName: 'Название',
      width: 350,
    },
    {
      field: 'adress',
      headerName: 'Адрес',
      width: 500,
      valueGetter: (params: GridValueGetterParams) =>
        `${params.row.postalCode}, ${params.row.region.name}, ${params.row.city.name}, ${params.row.streetName}, ${params.row.building}`,
    },
    {
      field: 'userNote',
      headerName: 'Заметка',
      width: 350,
    },
    {
      field: 'phoneNumber',
      headerName: 'Телефон',
      width: 350,
    },
  ];

  const onDelete = async (id: string): Promise<boolean> => {
    const res = await deleteOutlet(parseInt(id));
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
        name="Магазины"
        getData={getAllOutlets}
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
        name={'Создание точки распространения'}
        open={createOpen}
        form={<CreateOutlet setOpen={setCreateOpen} refresher={refreshFunction} />}
        setOpen={setCreateOpen}
      />
      <ModalFormDialog
        name={'Изменение точки распространения'}
        open={editOpen}
        form={<EditOutlet id={parseInt(selected)} setOpen={setEditOpen} refresher={refreshFunction} />}
        setOpen={setEditOpen}
      />
    </React.Fragment>
  );
};
