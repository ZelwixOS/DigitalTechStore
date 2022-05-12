import React from 'react';
import { GridColDef } from '@material-ui/data-grid';

import { TableBasement } from 'src/Components/Admin/Parts/TableBasement';
import { getTechParameters } from 'src/Requests/GetRequests';
import { deleteParameter } from 'src/Requests/DeleteRequests';
import ModalFormDialog from 'src/Components/Admin/Parts/ModalFormDialog';
import CreateParameter from 'src/Components/Admin/Parts/CreateParameter';
import EditParameter from 'src/Components/Admin/Parts/EditParameter';

export const TechParameterList = () => {
  const columns: GridColDef[] = [
    { field: 'id', headerName: 'ID', width: 300 },
    {
      field: 'name',
      headerName: 'Название',
      width: 400,
    },
    {
      field: 'important',
      headerName: 'Важный',
      width: 130,
      type: 'boolean',
    },
    {
      field: 'range',
      headerName: 'Диапазон',
      width: 140,
      type: 'boolean',
    },
    {
      field: 'minValue',
      headerName: 'Мин.знач.',
      width: 150,
      type: 'number',
    },
    {
      field: 'maxValue',
      headerName: 'Макс.знач.',
      width: 150,
      type: 'number',
    },
    {
      field: 'parameterBlockName',
      headerName: 'Блок',
      width: 400,
    },
  ];

  const onDelete = async (id: string): Promise<boolean> => {
    const res = await deleteParameter(id);
    if (res === 0) {
      setError('Не удалось удалить объект. Возможно, существуют зависимые блоки или объект используется в продукте.');
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
        name="Параметры"
        getData={getTechParameters}
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
        name={'Создание параметра'}
        open={createOpen}
        form={<CreateParameter setOpen={setCreateOpen} refresher={refreshFunction} />}
        setOpen={setCreateOpen}
      />
      <ModalFormDialog
        name={'Изменение параметра'}
        open={editOpen}
        form={<EditParameter id={selected} setOpen={setEditOpen} refresher={refreshFunction} />}
        setOpen={setEditOpen}
      />
    </React.Fragment>
  );
};
