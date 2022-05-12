import React, { useState } from 'react';
import { GridColDef } from '@material-ui/data-grid';

import { TableBasement } from 'src/Components/Admin/Parts/TableBasement';
import { getCommonCategories } from 'src/Requests/GetRequests';
import { deleteCommonCategory } from 'src/Requests/DeleteRequests';
import ModalFormDialog from 'src/Components/Admin/Parts/ModalFormDialog';
import CreateCommonCategory from 'src/Components/Admin/Parts/CreateCommonCategory';
import EditCommonCategory from 'src/Components/Admin/Parts/EditCommonCategory';

export const CommonCategoryList = () => {
  const columns: GridColDef[] = [
    { field: 'id', headerName: 'ID', width: 300 },
    {
      field: 'name',
      headerName: 'Название',
      width: 400,
    },
    {
      field: 'description',
      headerName: 'Описание',
      width: 600,
    },
  ];

  const onDelete = async (id: string): Promise<boolean> => {
    const res = await deleteCommonCategory(id);
    if (res === 0) {
      setError('Не удалось удалить объект. Возможно, существуют зависимые товары.');
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
        name="Обобщающие категории"
        getData={getCommonCategories}
        columns={columns}
        pageSize={10}
        createNew={createNew}
        editSelected={editSelected}
        deleteSelected={onDelete}
        open={open}
        setOpen={setOpen}
        error={error}
      />
      <ModalFormDialog
        name={'Создание общей категории'}
        open={createOpen}
        form={<CreateCommonCategory setOpen={setCreateOpen} refresher={refreshFunction} />}
        setOpen={setCreateOpen}
      />
      <ModalFormDialog
        name={'Изменение общей категории'}
        open={editOpen}
        form={<EditCommonCategory id={selected} setOpen={setEditOpen} refresher={refreshFunction} />}
        setOpen={setEditOpen}
      />
    </React.Fragment>
  );
};
