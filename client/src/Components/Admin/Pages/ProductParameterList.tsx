import React from 'react';
import { GridColDef } from '@material-ui/data-grid';

import { TableBasement } from 'src/Components/Admin/Parts/TableBasement';
import { getProductParameter } from 'src/Requests/GetRequests';
import { deleteProductParameter } from 'src/Requests/DeleteRequests';
import ModalFormDialog from 'src/Components/Admin/Parts/ModalFormDialog';
import CreateProductParameter from 'src/Components/Admin/Parts/CreateProductParameter';

export const ProductParameterList = () => {
  const columns: GridColDef[] = [
    { field: 'id', headerName: 'ID', width: 300 },
    {
      field: 'value',
      headerName: 'Значение',
      width: 200,
      type: 'number',
    },
    {
      field: 'parameterValue',
      headerName: 'Значение',
      width: 300,
    },
    {
      field: 'productName',
      headerName: 'Товар',
      width: 400,
    },
    {
      field: 'productId',
      headerName: 'Код',
      width: 300,
    },
    {
      field: 'parameterName',
      headerName: 'Параметр',
      width: 400,
    },
    {
      field: 'parameterId',
      headerName: 'Код',
      width: 300,
    },
  ];

  const onDelete = async (id: string): Promise<boolean> => {
    const res = await deleteProductParameter(id);
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
  const [refreshFunction, setRefrFun] = React.useState({ refresh: () => console.log('') });

  const createNew = (refrFun: () => void) => {
    setCreateOpen(true);
    setRefrFun({ refresh: refrFun });
  };

  return (
    <React.Fragment>
      <TableBasement
        name="Параметр продукта"
        getData={getProductParameter}
        columns={columns}
        pageSize={10}
        createNew={createNew}
        deleteSelected={onDelete}
        open={open}
        setOpen={setOpen}
        error={error}
      />
      <ModalFormDialog
        name={'Создание параметра продукта'}
        open={createOpen}
        form={<CreateProductParameter setOpen={setCreateOpen} refresher={refreshFunction} />}
        setOpen={setCreateOpen}
      />
    </React.Fragment>
  );
};
