import React, { useEffect } from 'react';
import Button from '@material-ui/core/Button';
import { createStyles, makeStyles, Theme } from '@material-ui/core/styles';
import { CircularProgress, Grid, MenuItem, Snackbar, TextField } from '@material-ui/core';
import { Alert } from '@mui/material';

import { getAllRegions, getCity } from 'src/Requests/GetRequests';
import { updateCity } from 'src/Requests/PutRequests';
import Region from 'src/Types/Region';

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

interface IEditCity {
  id: number;
  refresher?: IRefresher;
  setOpen: React.Dispatch<React.SetStateAction<boolean>>;
}

const EditCity: React.FC<IEditCity> = props => {
  const classes = useStyles();

  const getData = async (isMounted: boolean) => {
    setLoading(true);
    const res = await getAllRegions();
    const data = await getCity(props.id);
    if (isMounted) {
      setRegions(res);
      setCity({
        name: data.name,
        regionId: data.regionId,
      });
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
    name: city.name,
    regionId: city.regionId,
  });

  const [loading, setLoading] = React.useState(true);
  const [city, setCity] = React.useState({
    name: '',
    regionId: 0,
  });
  const [regions, setRegions] = React.useState<Region[]>([]);
  const [open, setOpen] = React.useState<boolean>(false);
  const [message, setMessage] = React.useState<string>('');

  const handleValueChange = (event: React.ChangeEvent<{ value: unknown }>) => {
    const data = cloneData();
    data.name = event.target.value as string;
    setCity(data);
  };

  const handleParameterChange = (event: React.ChangeEvent<{ value: unknown }>) => {
    const data = cloneData();
    data.regionId = parseInt(event.target.value as string);
    setCity(data);
  };

  const handleClose = (event?: React.SyntheticEvent, reason?: string) => {
    if (reason === 'clickaway') {
      return;
    }

    setOpen(false);
  };

  const onClick = async () => {
    if (city.name.length < 2) {
      setMessage('Введите корректное название');
      setOpen(true);
    } else if (city.regionId < 1) {
      setMessage('Укажите параметр');
      setOpen(true);
    } else {
      const res = await updateCity(props.id, city.name, city.regionId);
      if (res && props.refresher) {
        props.refresher.refresh();
        props.setOpen(false);
      } else {
        setMessage('Не удалось выполнить запрос');
        setOpen(true);
      }
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
            id="parameterName"
            className={classes.spaces}
            value={city.name}
            onChange={handleValueChange}
            label="Название"
            variant="outlined"
          />
          <TextField
            id="parameter"
            select
            label="Регион"
            className={classes.spaces}
            value={city.regionId}
            onChange={handleParameterChange}
            variant="outlined"
          >
            {regions.map(p => (
              <MenuItem key={p.id} value={p.id}>
                {p.name}
              </MenuItem>
            ))}
          </TextField>
          <Grid container justifyContent="flex-end">
            <Button type="submit" className={classes.spaces} color="primary" variant="contained" onClick={onClick}>
              Обновить
            </Button>
          </Grid>
        </React.Fragment>
      )}
    </Grid>
  );
};

export default EditCity;
