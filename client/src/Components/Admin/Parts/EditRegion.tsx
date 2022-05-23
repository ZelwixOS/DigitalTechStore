import React, { useEffect } from 'react';
import Button from '@material-ui/core/Button';
import { createStyles, makeStyles, Theme } from '@material-ui/core/styles';
import { Grid, Snackbar, TextField } from '@material-ui/core';
import { Alert } from '@mui/material';

import { getRegion } from 'src/Requests/GetRequests';
import { updateRegion } from 'src/Requests/PutRequests';

import { CityList } from './CityList';

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

interface IEditRegion {
  id: string;
  setOpen: React.Dispatch<React.SetStateAction<boolean>>;
  refresher?: IRefresher;
}

const EditRegion: React.FC<IEditRegion> = props => {
  const classes = useStyles();

  const getData = async (isMounted: boolean) => {
    const res = await getRegion(parseInt(props.id));
    if (isMounted) {
      setName(res.name);
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

  const [name, setName] = React.useState('');
  const [open, setOpen] = React.useState<boolean>(false);
  const [message, setMessage] = React.useState<string>('');

  const handleNameChange = (event: React.ChangeEvent<{ value: unknown }>) => {
    setName(event.target.value as string);
  };

  const handleClose = (event?: React.SyntheticEvent, reason?: string) => {
    if (reason === 'clickaway') {
      return;
    }

    setOpen(false);
  };

  const onClick = async () => {
    if (name.length < 2) {
      setMessage('Введите корректное название');
      setOpen(true);
    } else {
      const res = await updateRegion(props.id, name);
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
      <TextField
        id="regionName"
        className={classes.spaces}
        value={name}
        onChange={handleNameChange}
        label="Название"
        variant="outlined"
      />
      <CityList id={parseInt(props.id)} />
      <Grid container justifyContent="flex-end">
        <Button type="submit" className={classes.spaces} color="primary" variant="contained" onClick={onClick}>
          Обновить
        </Button>
      </Grid>
    </Grid>
  );
};

export default EditRegion;
