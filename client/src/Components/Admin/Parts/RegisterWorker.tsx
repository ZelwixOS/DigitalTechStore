import React, { useEffect } from 'react';
import Button from '@material-ui/core/Button';
import { createStyles, makeStyles, Theme } from '@material-ui/core/styles';
import { CircularProgress, Grid, MenuItem, Snackbar, TextField } from '@material-ui/core';
import { Alert } from '@mui/material';

import { registerWorker } from 'src/Requests/PostRequests';
import { getAllRegions, getOutlets, getWarehouses } from 'src/Requests/GetRequests';
import Outlet from 'src/Types/Outlet';
import Warehouse from 'src/Types/Warehouse';
import Region from 'src/Types/Region';
import City from 'src/Types/City';

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

interface IRegisterWorker {
  refresher?: IRefresher;
  setOpen: React.Dispatch<React.SetStateAction<boolean>>;
}

const RegisterWorker: React.FC<IRegisterWorker> = props => {
  const classes = useStyles();

  const getData = async (isMounted: boolean) => {
    setLoading(true);
    const regions = await getAllRegions();
    if (isMounted) {
      setRegions(regions);
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
    login: parameterData.login,
    password: parameterData.password,
    email: parameterData.email,
    phoneNumber: parameterData.phoneNumber,
    firstName: parameterData.firstName,
    secondName: parameterData.secondName,
    roleName: parameterData.roleName,
    regionId: parameterData.regionId,
    cityId: parameterData.cityId,
    outletId: parameterData.outletId,
    warehouseId: parameterData.warehouseId,
    outletWorker: parameterData.outletWorker,
  });

  const [parameterData, setParameterData] = React.useState({
    login: '',
    password: '',
    email: '',
    phoneNumber: '',
    firstName: '',
    secondName: '',
    roleName: '',
    regionId: 0,
    cityId: 0,
    outletId: 0,
    warehouseId: 0,
    outletWorker: true,
  });

  const [loading, setLoading] = React.useState(true);
  const [regions, setRegions] = React.useState<Region[]>([]);
  const [cities, setCities] = React.useState<City[]>([]);
  const [outlets, setOutlets] = React.useState<Outlet[]>([]);
  const [warehouses, setWarehouses] = React.useState<Warehouse[]>([]);
  const [open, setOpen] = React.useState<boolean>(false);
  const [message, setMessage] = React.useState<string>('');

  const handleLoginChange = (event: React.ChangeEvent<{ value: unknown }>) => {
    const data = cloneData();
    data.login = event.target.value as string;
    setParameterData(data);
  };

  const handlePasswordChange = (event: React.ChangeEvent<{ value: unknown }>) => {
    const data = cloneData();
    data.password = event.target.value as string;
    setParameterData(data);
  };

  const handleEmailChange = (event: React.ChangeEvent<{ value: unknown }>) => {
    const data = cloneData();
    data.email = event.target.value as string;
    setParameterData(data);
  };

  const handlePhoneNumberChange = (event: React.ChangeEvent<{ value: unknown }>) => {
    const data = cloneData();
    data.phoneNumber = event.target.value as string;
    setParameterData(data);
  };

  const handleFirstNameChange = (event: React.ChangeEvent<{ value: unknown }>) => {
    const data = cloneData();
    data.firstName = event.target.value as string;
    setParameterData(data);
  };

  const handleSecondNameChange = (event: React.ChangeEvent<{ value: unknown }>) => {
    const data = cloneData();
    data.secondName = event.target.value as string;
    setParameterData(data);
  };

  const handleRoleChange = (event: React.ChangeEvent<{ value: unknown }>) => {
    const data = cloneData();
    data.roleName = event.target.value as string;
    if (data.roleName === 'WarehouseWorker') {
      data.outletWorker = false;
    } else {
      data.outletWorker = true;
    }
    setParameterData(data);
  };

  const handleRegionChange = (event: React.ChangeEvent<{ value: unknown }>) => {
    const data = cloneData();
    data.regionId = event.target.value as number;
    const region = regions.find(r => r.id === data.regionId);
    if (region) {
      setCities(region.cities);
    }

    setParameterData(data);
  };

  const handleCityChange = async (event: React.ChangeEvent<{ value: unknown }>) => {
    const data = cloneData();
    data.cityId = event.target.value as number;
    if (data.outletWorker) {
      const ores = await getOutlets(data.cityId);
      setOutlets(ores);
    } else {
      const wres = await getWarehouses(data.cityId);
      setWarehouses(wres);
    }
    setParameterData(data);
  };

  const handleOutletChange = (event: React.ChangeEvent<{ value: unknown }>) => {
    const data = cloneData();
    data.outletId = event.target.value as number;
    setParameterData(data);
  };

  const handleWarehouseChange = (event: React.ChangeEvent<{ value: unknown }>) => {
    const data = cloneData();
    data.warehouseId = event.target.value as number;
    setParameterData(data);
  };

  const handleClose = (event?: React.SyntheticEvent, reason?: string) => {
    if (reason === 'clickaway') {
      return;
    }

    setOpen(false);
  };

  const onClick = async () => {
    try {
      const res = await registerWorker(
        parameterData.login,
        parameterData.password,
        parameterData.email,
        parameterData.phoneNumber,
        parameterData.firstName,
        parameterData.secondName,
        parameterData.roleName,
        parameterData.outletId,
        parameterData.warehouseId,
        parameterData.outletWorker,
      );

      if (res && props.refresher) {
        props.refresher.refresh();
        props.setOpen(false);
      } else {
        setMessage('Не удалось выполнить запрос');
        setOpen(true);
      }
    } catch (e) {
      setMessage(`Не удалось выполнить запрос: ${e}`);
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
          <Grid container direction="column" justifyContent="space-evenly">
            <Grid container direction="row" justifyContent="space-evenly">
              <TextField
                id="login"
                className={classes.spaces}
                value={parameterData.login}
                onChange={handleLoginChange}
                label="Логин"
                variant="outlined"
              />
              <TextField
                id="password"
                className={classes.spaces}
                value={parameterData.password}
                onChange={handlePasswordChange}
                label="Пароль"
                variant="outlined"
                type="password"
              />
            </Grid>
            <Grid container direction="row" justifyContent="space-evenly">
              <TextField
                id="firstName"
                className={classes.spaces}
                value={parameterData.firstName}
                onChange={handleFirstNameChange}
                label="Имя"
                variant="outlined"
              />
              <TextField
                id="secondName"
                className={classes.spaces}
                value={parameterData.secondName}
                onChange={handleSecondNameChange}
                label="Фамилия"
                variant="outlined"
              />
            </Grid>
            <Grid container direction="row" justifyContent="space-evenly">
              <TextField
                id="email"
                className={classes.spaces}
                value={parameterData.email}
                onChange={handleEmailChange}
                label="Email"
                variant="outlined"
              />
              <TextField
                id="phone"
                className={classes.spaces}
                value={parameterData.phoneNumber}
                onChange={handlePhoneNumberChange}
                label="Телефон"
                variant="outlined"
              />
            </Grid>
            <TextField
              id="role"
              select
              label="Роль"
              className={classes.spaces}
              value={parameterData.roleName}
              onChange={handleRoleChange}
            >
              <MenuItem key={0} value={'Courier'}>
                Курьер
              </MenuItem>
              <MenuItem key={1} value={'Manager'}>
                Менеджер
              </MenuItem>
              <MenuItem key={2} value={'ShopAssistant'}>
                Продавец
              </MenuItem>
              <MenuItem key={3} value={'WarehouseWorker'}>
                Работник склада
              </MenuItem>
            </TextField>
            <TextField
              id="region"
              select
              label="Регион"
              className={classes.spaces}
              value={parameterData.regionId}
              onChange={handleRegionChange}
              variant="outlined"
            >
              {regions.map(p => (
                <MenuItem key={p.id} value={p.id}>
                  {p.name}
                </MenuItem>
              ))}
            </TextField>
            {parameterData.regionId !== 0 && (
              <TextField
                id="city"
                select
                label="Город"
                className={classes.spaces}
                value={parameterData.cityId}
                onChange={handleCityChange}
                variant="outlined"
              >
                {cities.map(p => (
                  <MenuItem key={p.id} value={p.id}>
                    {p.name}
                  </MenuItem>
                ))}
              </TextField>
            )}
            {parameterData.cityId !== 0 && (
              <React.Fragment>
                {parameterData.outletWorker ? (
                  <TextField
                    id="outlet"
                    select
                    label="Магазин"
                    className={classes.spaces}
                    value={parameterData.outletId}
                    onChange={handleOutletChange}
                    variant="outlined"
                  >
                    {outlets.map(p => (
                      <MenuItem key={p.id} value={p.id}>
                        {p.name}
                      </MenuItem>
                    ))}
                  </TextField>
                ) : (
                  <TextField
                    id="warehouse"
                    select
                    label="Склад"
                    className={classes.spaces}
                    value={parameterData.warehouseId}
                    onChange={handleWarehouseChange}
                    variant="outlined"
                  >
                    {warehouses.map(p => (
                      <MenuItem key={p.id} value={p.id}>
                        {p.name}
                      </MenuItem>
                    ))}
                  </TextField>
                )}
              </React.Fragment>
            )}
          </Grid>
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

export default RegisterWorker;
