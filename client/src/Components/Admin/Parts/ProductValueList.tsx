import React from 'react';

import { ProductTable } from 'src/Components/Admin/Parts/ProductTable';
import { deleteProduct } from 'src/Requests/DeleteRequests';
import ModalFormDialog from 'src/Components/Admin/Parts/ModalFormDialog';
import CreateProduct from 'src/Components/Admin/Parts/CreateProduct';
import EditProduct from 'src/Components/Admin/Parts/EditProduct';

export const ProductValueList = () => {
  const onDelete = async (id: string): Promise<boolean> => {
    const res = await deleteProduct(id);
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
      <ProductTable
        createNew={createNew}
        editSelected={editSelected}
        setSelected={setSelected}
        deleteSelected={onDelete}
        open={open}
        setOpen={setOpen}
        error={error}
      />
      <ModalFormDialog
        name={'Создание продукта'}
        open={createOpen}
        form={<CreateProduct setOpen={setCreateOpen} refresher={refreshFunction} />}
        setOpen={setCreateOpen}
      />
      <ModalFormDialog
        name={'Изменение продукта'}
        open={editOpen}
        form={<EditProduct id={selected} setOpen={setEditOpen} refresher={refreshFunction} />}
        setOpen={setEditOpen}
      />
    </React.Fragment>
  );
};
