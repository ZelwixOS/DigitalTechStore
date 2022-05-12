import React, { useEffect } from 'react';
import Button from '@material-ui/core/Button';
import { createStyles, makeStyles, Theme } from '@material-ui/core/styles';
import { CircularProgress, Grid, MenuItem, Snackbar, TextField } from '@material-ui/core';
import { Alert } from '@mui/material';

import { createProductParameter } from 'src/Requests/PostRequests';
import { getTechParameters, getProducts } from 'src/Requests/GetRequests';
import Parameter from 'src/Types/Parameter';
import Product from 'src/Types/Product';

interface IRefresher {
  refresh: () => void;
}

const useStyles = makeStyles((theme: Theme) =>
  createStyles({
    spaces: {
      margin: theme.spacing(2),
    },
  }),
);

interface ICreateProductParameter {
  refresher?: IRefresher;
  setOpen: React.Dispatch<React.SetStateAction<boolean>>;
}

interface IProductValue {
  productId: string;
  value: number | null;
  parameterId: string;
  parameterValueId: string | null;
}

const CreateProductParameter: React.FC<ICreateProductParameter> = props => {
  const classes = useStyles();

  const getData = async (isMounted: boolean) => {
    setLoading(true);
    const prods = await getProducts(1, 25, '1', [0, 0]);
    const res = await getTechParameters();
    if (isMounted) {
      setParameters(res);
      setProducts(prods.container);
      setLoading(false);
    }
  };

  const refreshData = () => {
    let isMounted = true;
    getData(isMounted);

    return () => {
      isMounted = false;
    };
  };

  useEffect(() => {
    refreshData();
  }, []);

  const cloneData = () => ({
    productId: parameterData.productId,
    value: parameterData.value,
    parameterId: parameterData.parameterId,
    parameterValueId: parameterData.parameterValueId,
  });

  const [loading, setLoading] = React.useState(true);
  const [parameterData, setParameterData] = React.useState<IProductValue>({
    productId: '',
    value: 0,
    parameterId: '',
    parameterValueId: '',
  });
  const [parameters, setParameters] = React.useState<Parameter[]>([]);
  const [parameter, setParameter] = React.useState<Parameter>();
  const [products, setProducts] = React.useState<Product[]>([]);
  const [open, setOpen] = React.useState<boolean>(false);
  const [message, setMessage] = React.useState<string>('');

  const handleProductIdChange = (event: React.ChangeEvent<{ value: unknown }>) => {
    const data = cloneData();
    data.productId = event.target.value as string;
    setParameterData(data);
  };

  const handleValueChange = (event: React.ChangeEvent<{ value: unknown }>) => {
    const data = cloneData();
    data.value = (event.target.value as number) / 1;
    setParameterData(data);
  };

  const handleParameterIdChange = (event: React.ChangeEvent<{ value: unknown }>) => {
    const data = cloneData();
    data.parameterId = event.target.value as string;
    const found = parameters.find(p => p.id === data.parameterId);
    if (found) {
      setParameter(found);
      if (found.range) {
        data.parameterValueId = null;
      }
    }
    setParameterData(data);
  };

  const handleParameterValueIdChange = (event: React.ChangeEvent<{ value: unknown }>) => {
    const data = cloneData();
    data.parameterValueId = event.target.value as string;
    setParameterData(data);
  };

  const handleClose = (event?: React.SyntheticEvent, reason?: string) => {
    if (reason === 'clickaway') {
      return;
    }

    setOpen(false);
  };

  const onClick = async () => {
    const res = await createProductParameter(
      parameterData.productId,
      parameterData.value,
      parameterData.parameterId,
      parameterData.parameterValueId,
    );
    if (res && props.refresher) {
      props.refresher.refresh();
    } else {
      setMessage('Не удалось выполнить запрос');
      setOpen(true);
    }
  };

  return (
    <Grid container direction="column" justifyContent="center">
      <Snackbar
        anchorOrigin={{ vertical: 'top', horizontal: 'center' }}
        open={open}
        autoHideDuration={6000}
        onClose={handleClose}
      >
        <Alert onClose={handleClose} severity="warning">
          {message}
        </Alert>
      </Snackbar>
      {loading ? (
        <Grid container alignContent="stretch" justifyContent="center">
          <CircularProgress />
        </Grid>
      ) : (
        <React.Fragment>
          <TextField
            id="product"
            select
            label="Продукт"
            className={classes.spaces}
            value={parameterData.productId}
            onChange={handleProductIdChange}
            variant="outlined"
          >
            {products.map(p => (
              <MenuItem key={p.id} value={p.id}>
                {p.name}
              </MenuItem>
            ))}
          </TextField>
          <TextField
            id="parameter"
            select
            label="Параметр"
            className={classes.spaces}
            value={parameterData.parameterId}
            onChange={handleParameterIdChange}
            variant="outlined"
          >
            {parameters.map(p => (
              <MenuItem key={p.id} value={p.id}>
                {p.name}
              </MenuItem>
            ))}
          </TextField>
          {parameter && parameter.range ? (
            <TextField
              id="value"
              className={classes.spaces}
              value={parameterData.value}
              onChange={handleValueChange}
              label="Значение"
              variant="outlined"
              type="number"
            />
          ) : (
            <TextField
              id="parameterValue"
              select
              label="Значение"
              className={classes.spaces}
              value={parameterData.parameterValueId}
              onChange={handleParameterValueIdChange}
              variant="outlined"
            >
              {parameter?.parameterValues.map(p => (
                <MenuItem key={p.id} value={p.id}>
                  {p.value}
                </MenuItem>
              ))}
            </TextField>
          )}
          <Grid container justifyContent="flex-end">
            <Button type="submit" className={classes.spaces} color="primary" variant="contained" onClick={onClick}>
              Создать
            </Button>
          </Grid>
        </React.Fragment>
      )}
    </Grid>
  );
};

export default CreateProductParameter;
