import React from 'react';
import { GridColDef } from '@material-ui/data-grid';

import { getRegionCities } from 'src/Requests/GetRequests';
import ModalFormDialog from 'src/Components/Admin/Parts/ModalFormDialog';
import { deleteCity } from 'src/Requests/DeleteRequests';
import CreateCity from 'src/Components/Admin/Parts/CreateCity';

import { TableStructure } from './TableStructure';
import EditCity from './EditCity';

interface ICityList {
  id: number;
}

export const CityList: React.FC<ICityList> = props => {
  const columns: GridColDef[] = [
    { field: 'id', headerName: 'ID', width: 100 },
    {
      field: 'name',
      headerName: 'Название',
      width: 250,
    },
  ];

  const onDelete = async (id: string): Promise<boolean> => {
    const res = await deleteCity(parseInt(id));
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

  const getCities = getRegionCities.bind(this, props.id);

  return (
    <React.Fragment>
      <TableStructure
        name="Города"
        getData={getCities}
        columns={columns}
        pageSize={5}
        deleteSelected={onDelete}
        createNew={createNew}
        editSelected={editSelected}
        open={open}
        setOpen={setOpen}
        error={error}
        compact
      />
      <ModalFormDialog
        name={'Создание города региона'}
        open={createOpen}
        form={<CreateCity regionId={props.id} setOpen={setCreateOpen} refresher={refreshFunction} />}
        setOpen={setCreateOpen}
      />
      <ModalFormDialog
        name={'Изменение города региона'}
        open={editOpen}
        form={<EditCity id={parseInt(selected)} setOpen={setEditOpen} refresher={refreshFunction} />}
        setOpen={setEditOpen}
      />
    </React.Fragment>
  );
};
