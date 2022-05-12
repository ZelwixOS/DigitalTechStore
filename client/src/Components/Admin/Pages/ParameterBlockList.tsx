import React from 'react';
import { GridColDef } from '@material-ui/data-grid';

import { TableBasement } from 'src/Components/Admin/Parts/TableBasement';
import { getParamBlocks } from 'src/Requests/GetRequests';
import { deleteParameterBlock } from 'src/Requests/DeleteRequests';
import ModalFormDialog from 'src/Components/Admin/Parts/ModalFormDialog';
import CreateParameterBlock from 'src/Components/Admin/Parts/CreateParameterBlock';
import EditParameterBlock from 'src/Components/Admin/Parts/EditParameterBlock';

export const ParameterBlockList = () => {
  const columns: GridColDef[] = [
    { field: 'id', headerName: 'ID', width: 300 },
    {
      field: 'name',
      headerName: 'Название',
      width: 400,
    },
  ];

  const onDelete = async (id: string): Promise<boolean> => {
    const res = await deleteParameterBlock(id);
    if (res === 0) {
      setError(
        'Не удалось удалить объект. Возможно, существуют зависимые характеристики или объект используется в категории.',
      );
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
        name="Блоки параметров"
        getData={getParamBlocks}
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
        name={'Создание блока параметров'}
        open={createOpen}
        form={<CreateParameterBlock setOpen={setCreateOpen} refresher={refreshFunction} />}
        setOpen={setCreateOpen}
      />
      <ModalFormDialog
        name={'Изменение блока параметров'}
        open={editOpen}
        form={<EditParameterBlock id={selected} setOpen={setEditOpen} refresher={refreshFunction} />}
        setOpen={setEditOpen}
      />
    </React.Fragment>
  );
};
