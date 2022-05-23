import React from 'react';
import { GridColDef } from '@material-ui/data-grid';

import NavigationBar from 'src/Components/Admin/Parts/NavigationBar';

import { TableStructure } from './TableStructure';

interface ITableBasement<Type> {
  name: string;
  getData: () => Promise<Type[]>;
  columns: GridColDef[];
  pageSize: number;
  setSelected?: (id: string) => void;
  deleteSelected?: (id: string) => Promise<boolean>;
  editSelected?: (selectedId: string, refrFun: () => void) => void;
  createNew?: (update: () => void) => void;
  open?: boolean;
  setOpen?: React.Dispatch<React.SetStateAction<boolean>>;
  error?: string;
}

export const TableBasement = <Type,>(props: ITableBasement<Type>) => (
  <React.Fragment>
    <NavigationBar />
    <TableStructure
      name={props.name}
      getData={props.getData}
      columns={props.columns}
      pageSize={props.pageSize}
      setSelected={props.setSelected}
      deleteSelected={props.deleteSelected}
      editSelected={props.editSelected}
      createNew={props.createNew}
      open={props.open}
      setOpen={props.setOpen}
      error={props.error}
    />
  </React.Fragment>
);
