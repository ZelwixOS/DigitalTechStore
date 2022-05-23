import React from 'react';
import { GridColDef } from '@material-ui/data-grid';

import { getCategories } from 'src/Requests/GetRequests';
import ModalFormDialog from 'src/Components/Admin/Parts/ModalFormDialog';
import { deleteCategory } from 'src/Requests/DeleteRequests';
import CreateCategory from 'src/Components/Admin/Parts/CreateCategory';
import EditCategory from 'src/Components/Admin/Parts/EditCategory';

import { TableStructure } from './TableStructure';

interface ICompactCategoryList {
  id: string;
  name: string;
}

export const CompactCategoryList: React.FC<ICompactCategoryList> = props => {
  const columns: GridColDef[] = [
    { field: 'id', headerName: 'ID', width: 300 },
    {
      field: 'name',
      headerName: 'Название',
      width: 350,
    },
    {
      field: 'deliveryPrice',
      headerName: 'Цена доставки',
      type: 'number',
      width: 150,
    },
  ];

  const onDelete = async (id: string): Promise<boolean> => {
    const res = await deleteCategory(id);
    if (res === 0) {
      setError('Не удалось удалить объект. Возможно, существуют зависимые категории.');
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

  const getData = getCategories.bind(this, props.name);

  return (
    <React.Fragment>
      <TableStructure
        name="Категории"
        getData={getData}
        columns={columns}
        pageSize={10}
        deleteSelected={onDelete}
        createNew={createNew}
        editSelected={editSelected}
        open={open}
        setOpen={setOpen}
        error={error}
        compact
      />
      <ModalFormDialog
        name={'Создание категории'}
        open={createOpen}
        form={<CreateCategory commonCategoryId={props.id} setOpen={setCreateOpen} refresher={refreshFunction} />}
        setOpen={setCreateOpen}
      />
      <ModalFormDialog
        name={'Изменение категории'}
        open={editOpen}
        form={<EditCategory id={selected} setOpen={setEditOpen} refresher={refreshFunction} />}
        setOpen={setEditOpen}
      />
    </React.Fragment>
  );
};
