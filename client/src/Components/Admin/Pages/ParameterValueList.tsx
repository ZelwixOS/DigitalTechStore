import React from 'react';
import { GridColDef } from '@material-ui/data-grid';

import { TableBasement } from 'src/Components/Admin/Parts/TableBasement';
import { getParameterValues } from 'src/Requests/GetRequests';
import { deleteParameterValue } from 'src/Requests/DeleteRequests';
import ModalFormDialog from 'src/Components/Admin/Parts/ModalFormDialog';
import CreateParameterValue from 'src/Components/Admin/Parts/CreateParameterValue';
import EditParameterValue from 'src/Components/Admin/Parts/EditParameterValue';

export const ParameterValueList = () => {
  const columns: GridColDef[] = [
    { field: 'id', headerName: 'ID', width: 300 },
    {
      field: 'value',
      headerName: 'Значение',
      width: 500,
    },
    {
      field: 'parameterName',
      headerName: 'Параметр',
      width: 500,
    },
    {
      field: 'parameterId',
      headerName: 'Код',
      width: 300,
    },
  ];

  const onDelete = async (id: string): Promise<boolean> => {
    const res = await deleteParameterValue(id);
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
        name="Значения параметров"
        getData={getParameterValues}
        columns={columns}
        pageSize={12}
        createNew={createNew}
        editSelected={editSelected}
        deleteSelected={onDelete}
        open={open}
        setOpen={setOpen}
        error={error}
      />
      <ModalFormDialog
        name={'Создание значения параметра'}
        open={createOpen}
        form={<CreateParameterValue setOpen={setCreateOpen} refresher={refreshFunction} />}
        setOpen={setCreateOpen}
      />
      <ModalFormDialog
        name={'Изменение значения параметра'}
        open={editOpen}
        form={<EditParameterValue id={selected} setOpen={setEditOpen} refresher={refreshFunction} />}
        setOpen={setEditOpen}
      />
    </React.Fragment>
  );
};
