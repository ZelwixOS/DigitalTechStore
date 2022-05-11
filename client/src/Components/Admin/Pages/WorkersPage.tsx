import React from 'react';
import { GridColDef } from '@material-ui/data-grid';

import RegisterWorker from 'src/Components/Admin/Parts/RegisterWorker';
import { getWorkers } from 'src/Requests/GetRequests';
import ModalFormDialog from 'src/Components/Admin/Parts/ModalFormDialog';
import { UserTable } from 'src/Components/Admin/Parts/UserTable';
import { banUser, unbanUser } from 'src/Requests/PostRequests';
import EditWorkerInfo from 'src/Components/Admin/Parts/EditWorkerInfo';

export const WorkersPage = () => {
  const columns: GridColDef[] = [
    { field: 'id', headerName: 'ID', width: 300 },
    {
      field: 'userName',
      headerName: 'Логин',
      width: 200,
    },
    {
      field: 'firstName',
      headerName: 'Имя',
      width: 200,
    },
    {
      field: 'secondName',
      headerName: 'Фамилия',
      width: 200,
    },
    {
      field: 'role',
      headerName: 'Роль',
      width: 130,
    },
    {
      field: 'email',
      headerName: 'Роль',
      width: 230,
    },
    {
      field: 'phoneNumber',
      headerName: 'Телефон',
      width: 150,
    },
    {
      field: 'unitName',
      headerName: 'Отделение',
      width: 300,
    },
    {
      field: 'outletId',
      headerName: 'Код магазина',
      width: 120,
    },
    {
      field: 'warehouseId',
      headerName: 'Код склада',
      width: 120,
    },
    {
      field: 'banned',
      headerName: 'Бан',
      width: 120,
      type: 'boolean',
    },
  ];

  const onBan = async (id: string): Promise<boolean> => {
    const res = await banUser(id);
    if (res === 0) {
      setError('Не удалось удалить объект. Возможно, существуют зависимые блоки или объект используется в продукте.');
      setOpen(true);
      return false;
    }

    return true;
  };

  const onUnban = async (id: string): Promise<boolean> => {
    const res = await unbanUser(id);
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
      <UserTable
        name="Работники"
        getData={getWorkers}
        columns={columns}
        pageSize={12}
        createNew={createNew}
        editSelected={editSelected}
        banSelected={onBan}
        unbanSelected={onUnban}
        open={open}
        setOpen={setOpen}
        error={error}
      />
      <ModalFormDialog
        name={'Регистрация работника'}
        open={createOpen}
        form={<RegisterWorker setOpen={setCreateOpen} refresher={refreshFunction} />}
        setOpen={setCreateOpen}
      />
      <ModalFormDialog
        name={'Изменение работника'}
        open={editOpen}
        form={<EditWorkerInfo id={selected} setOpen={setEditOpen} refresher={refreshFunction} />}
        setOpen={setEditOpen}
      />
    </React.Fragment>
  );
};
