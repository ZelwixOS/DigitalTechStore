import React, { useEffect, useState } from 'react';
import { DataGrid, GridColDef, GridSelectionModel } from '@material-ui/data-grid';
import { createStyles, Fab, Grid, makeStyles, Snackbar, TextField, Theme, Typography } from '@material-ui/core';
import AddIcon from '@material-ui/icons/Add';
import ContentCopyIcon from '@mui/icons-material/ContentCopy';
import EditIcon from '@material-ui/icons/Edit';
import DeleteIcon from '@material-ui/icons/Delete';
import PublishedWithChangesIcon from '@mui/icons-material/PublishedWithChanges';
import UnpublishedIcon from '@mui/icons-material/Unpublished';
import { Alert } from '@material-ui/lab';

import NavigationBar from 'src/Components/Admin/Parts/NavigationBar';
import { getSearchedProducts } from 'src/Requests/GetRequests';
import Product from 'src/Types/Product';
import { cloneProduct, publishProduct, unpublishProduct } from 'src/Requests/PostRequests';

const useStyles = makeStyles((theme: Theme) =>
  createStyles({
    floating: {
      padding: theme.spacing(3),
    },
    fabBlue: {
      color: theme.palette.common.white,
      backgroundColor: '#2979ff',
      '&:hover': {
        backgroundColor: '#5393ff',
      },
    },
    fabSkyBlue: {
      color: theme.palette.common.white,
      backgroundColor: '#0a00b6',
      '&:hover': {
        backgroundColor: '#6200ea',
      },
    },
    fabGreen: {
      color: theme.palette.common.white,
      backgroundColor: '#14D100',
      '&:hover': {
        backgroundColor: '#329D27',
      },
    },
    fabRed: {
      color: theme.palette.common.white,
      backgroundColor: '#ff1744',
      '&:hover': {
        backgroundColor: '#b2102f',
      },
    },
    fabOrange: {
      color: theme.palette.common.white,
      backgroundColor: '#b91400',
      '&:hover': {
        backgroundColor: '#f4511e',
      },
    },
    fabViolet: {
      color: theme.palette.common.white,
      backgroundColor: '#673ab7',
      '&:hover': {
        backgroundColor: '#8561c5',
      },
    },
    spaces: {
      margin: theme.spacing(2),
    },
  }),
);

interface IProductTable {
  setSelected?: (id: string) => void;
  deleteSelected?: (id: string) => Promise<boolean>;
  editSelected?: (selectedId: string, refrFun: () => void) => void;
  createNew?: (update: () => void) => void;
  open?: boolean;
  setOpen?: React.Dispatch<React.SetStateAction<boolean>>;
  error?: string;
}

export const ProductTable = (props: IProductTable) => {
  const classes = useStyles();
  const pageOnCount = 8;

  const columns: GridColDef[] = [
    { field: 'id', sortable: false, headerName: 'ID', width: 300 },
    {
      field: 'name',
      headerName: 'Название',
      sortable: false,
      width: 500,
    },
    {
      field: 'price',
      headerName: 'Цена',
      sortable: false,
      width: 150,
    },
    {
      field: 'priceWithoutDiscount',
      headerName: 'Цена без скидки',
      sortable: false,
      width: 150,
    },
    {
      field: 'mark',
      headerName: 'Оценка',
      sortable: false,
      width: 130,
      type: 'number',
    },
    {
      field: 'vendorCode',
      headerName: 'Артикул',
      sortable: false,
      width: 150,
    },
    {
      field: 'category',
      headerName: 'Категория',
      sortable: false,
      width: 160,
      valueGetter: params => `${params.row.category?.name}`,
    },
    {
      field: 'published',
      headerName: 'Опубликован',
      sortable: false,
      width: 150,
      type: 'boolean',
    },
  ];

  const getDataPage = async (isMounted: boolean, page: number, search: string) => {
    const res = await getSearchedProducts(page, pageOnCount, 'name', search);
    if (isMounted) {
      const d = [...data];
      for (let i = 0; i < pageOnCount && i < res.container.length; i++) {
        d[(page - 1) * pageOnCount + i] = res.container[i];
      }

      setData(d);
      setPageCount(res.maxPage);
    }
  };

  useEffect(() => {
    refreshData();
  }, []);

  const refreshData = () => {
    let isMounted = true;
    getDataPage(isMounted, pageCount, search);

    return () => {
      isMounted = false;
    };
  };

  const refreshByPage = (page: number) => {
    let isMounted = true;
    getDataPage(isMounted, page, search);

    return () => {
      isMounted = false;
    };
  };

  const refreshByFilter = (searchQuery: string) => {
    let isMounted = true;
    getDataPage(isMounted, 1, searchQuery);

    return () => {
      isMounted = false;
    };
  };

  const [data, setData] = useState<Product[]>([]);
  const [pageNum, setPageNum] = useState<number>(0);
  const [selectedItem, setSelectedItem] = useState<string>();
  const [pageCount, setPageCount] = useState<number>(1);
  const [search, setSearch] = useState<string>('');

  const onPageChange = async (page: number, details?: unknown) => {
    const pg = page + 1;
    setPageNum(page);
    refreshByPage(pg);
  };

  const onSelection = (selectionModel: GridSelectionModel, details?: unknown) => {
    if (selectionModel.length > 0) {
      setSelectedItem(selectionModel[0] as string);
      if (props.setSelected) {
        props.setSelected(selectionModel[0] as string);
      }
    }
  };

  const onDelete = async () => {
    if (selectedItem && props.deleteSelected) {
      const res = await props.deleteSelected(selectedItem);
      if (res) {
        refreshData();
      }
    }
  };

  const onCreate = async () => {
    if (props.createNew) {
      props.createNew(refreshData.bind(this));
    }
  };

  const onClone = async () => {
    const res = await cloneProduct(selectedItem as string);
    if (res) {
      refreshData();
    }
  };

  const onPublish = async () => {
    const res = await publishProduct(selectedItem as string);
    if (res > 0) {
      refreshData();
    }
  };

  const onUnpublish = async () => {
    const res = await unpublishProduct(selectedItem as string);
    if (res > 0) {
      refreshData();
    }
  };

  const onEdit = async () => {
    if (selectedItem && props.editSelected) {
      props.editSelected(selectedItem, refreshData.bind(this));
    }
  };

  const handleClose = (event?: React.SyntheticEvent, reason?: string) => {
    if (reason === 'clickaway') {
      return;
    }

    if (props.setOpen) {
      props.setOpen(false);
    }
  };

  const handleSearchChange = (event: React.ChangeEvent<{ value: unknown }>) => {
    const query = event.target.value as string;
    setData([]);
    setSearch(query);
    refreshByFilter(query);
  };

  return (
    <React.Fragment>
      <NavigationBar />
      {props.setOpen && props.error && (
        <Snackbar
          anchorOrigin={{ vertical: 'top', horizontal: 'center' }}
          open={props.open}
          autoHideDuration={6000}
          onClose={handleClose}
        >
          <Alert onClose={handleClose} severity="error">
            {props.error}
          </Alert>
        </Snackbar>
      )}
      <Grid direction="column" className={classes.floating} container alignContent="stretch">
        <Typography align="center" variant="h4">
          Продукты
        </Typography>
        <TextField
          id="categoryDeliveryPrice"
          variant="outlined"
          className={classes.spaces}
          label="Фильтр названий"
          value={search}
          onChange={handleSearchChange}
        />
        <DataGrid
          style={{ minHeight: 545 }}
          rows={data}
          page={pageNum}
          rowCount={pageCount * pageOnCount}
          columns={columns}
          pageSize={pageOnCount}
          rowsPerPageOptions={[pageOnCount]}
          onPageChange={onPageChange}
          onSelectionModelChange={onSelection}
        />
        <Grid className={classes.floating} direction="row" container justifyContent="flex-end" alignContent="stretch">
          <Grid container justifyContent="center" item xs={12} sm={4}>
            <Fab
              disabled={!props.createNew}
              variant="extended"
              className={classes.fabBlue}
              color="inherit"
              aria-label="add"
              onClick={onCreate}
            >
              <AddIcon />
              Добавить
            </Fab>
          </Grid>
          <Grid container justifyContent="center" item xs={12} sm={4}>
            <Fab
              disabled={!selectedItem || selectedItem === '' || !props.editSelected}
              variant="extended"
              className={classes.fabViolet}
              color="inherit"
              aria-label="edit"
              onClick={onEdit}
            >
              <EditIcon />
              Изменить
            </Fab>
          </Grid>
          <Grid container justifyContent="center" item xs={12} sm={4}>
            {selectedItem && data.find(s => s.id === selectedItem)?.published ? (
              <Fab
                disabled={!selectedItem || selectedItem === ''}
                variant="extended"
                className={classes.fabOrange}
                color="inherit"
                aria-label="delete"
                onClick={onUnpublish}
              >
                <UnpublishedIcon />
                Отменить публикацию
              </Fab>
            ) : (
              <Fab
                disabled={!selectedItem || selectedItem === ''}
                variant="extended"
                className={classes.fabGreen}
                color="inherit"
                aria-label="delete"
                onClick={onPublish}
              >
                <PublishedWithChangesIcon />
                Опубликовать
              </Fab>
            )}
          </Grid>
        </Grid>
        <Grid direction="row" container justifyContent="flex-end" alignContent="stretch">
          <Grid container justifyContent="center" item xs={12} sm={6}>
            <Fab
              disabled={!selectedItem || selectedItem === ''}
              variant="extended"
              className={classes.fabSkyBlue}
              color="inherit"
              aria-label="clone"
              onClick={onClone}
            >
              <ContentCopyIcon />
              Клонировать
            </Fab>
          </Grid>
          <Grid container justifyContent="center" item xs={12} sm={6}>
            <Fab
              disabled={!selectedItem || selectedItem === '' || !props.deleteSelected}
              variant="extended"
              className={classes.fabRed}
              color="inherit"
              aria-label="delete"
              onClick={onDelete}
            >
              <DeleteIcon />
              Удалить
            </Fab>
          </Grid>
        </Grid>
      </Grid>
    </React.Fragment>
  );
};
