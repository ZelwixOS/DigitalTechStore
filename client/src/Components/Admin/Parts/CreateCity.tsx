import React, { useEffect } from 'react';
import Button from '@material-ui/core/Button';
import { createStyles, makeStyles, Theme } from '@material-ui/core/styles';
import { CircularProgress, Grid, MenuItem, Snackbar, TextField } from '@material-ui/core';
import { Alert } from '@mui/material';

import { createCity } from 'src/Requests/PostRequests';
import { getAllRegions } from 'src/Requests/GetRequests';
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

interface ICreateCity {
  regionId?: number;
  refresher?: IRefresher;
  setOpen: React.Dispatch<React.SetStateAction<boolean>>;
}

const CreateCity: React.FC<ICreateCity> = props => {
  const classes = useStyles();

  const getData = async (isMounted: boolean) => {
    setLoading(true);
    const res = await getAllRegions();
    if (isMounted) {
      setRegions(res);
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
    name: parameterData.name,
    regionId: props.regionId ?? parameterData.regionId,
  });

  const [loading, setLoading] = React.useState(true);
  const [parameterData, setParameterData] = React.useState({
    name: '',
    regionId: props.regionId ?? 0,
  });

  const [regions, setRegions] = React.useState<Region[]>([]);
  const [open, setOpen] = React.useState<boolean>(false);
  const [message, setMessage] = React.useState<string>('');

  const handleNameChange = (event: React.ChangeEvent<{ value: unknown }>) => {
    const data = cloneData();
    data.name = event.target.value as string;
    setParameterData(data);
  };

  const handleRegionChange = (event: React.ChangeEvent<{ value: unknown }>) => {
    const data = cloneData();
    data.regionId = parseInt(event.target.value as string);
    setParameterData(data);
  };

  const handleClose = (event?: React.SyntheticEvent, reason?: string) => {
    if (reason === 'clickaway') {
      return;
    }

    setOpen(false);
  };

  const onClick = async () => {
    if (parameterData.name.length < 2) {
      setMessage('Введите корректное название');
      setOpen(true);
    } else if (parameterData.regionId < 0) {
      setMessage('Укажите регион');
      setOpen(true);
    } else {
      const res = await createCity(parameterData.name, parameterData.regionId);
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
            value={parameterData.name}
            onChange={handleNameChange}
            label="Значение"
            variant="outlined"
          />
          <TextField
            id="parameter"
            select
            label="Параметр"
            className={classes.spaces}
            value={parameterData.regionId}
            onChange={handleRegionChange}
            variant="outlined"
            disabled={props.regionId !== undefined}
          >
            {regions.map(p => (
              <MenuItem key={p.id} value={p.id}>
                {p.name}
              </MenuItem>
            ))}
          </TextField>
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

export default CreateCity;
