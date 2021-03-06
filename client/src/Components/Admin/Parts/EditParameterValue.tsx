import React, { useEffect } from 'react';
import Button from '@material-ui/core/Button';
import { createStyles, makeStyles, Theme } from '@material-ui/core/styles';
import { CircularProgress, Grid, MenuItem, Snackbar, TextField } from '@material-ui/core';
import { Alert } from '@mui/material';

import { getParameterValue, getTechListParameters } from 'src/Requests/GetRequests';
import Parameter from 'src/Types/Parameter';
import { updateParameterValue } from 'src/Requests/PutRequests';

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

interface IEditParameterValue {
  id: string;
  refresher?: IRefresher;
  setOpen: React.Dispatch<React.SetStateAction<boolean>>;
}

const EditParameterValue: React.FC<IEditParameterValue> = props => {
  const classes = useStyles();

  const getData = async (isMounted: boolean) => {
    setLoading(true);
    const res = await getTechListParameters();
    const data = await getParameterValue(props.id);
    if (isMounted) {
      setParameters(res);
      setParameterData({
        value: data.value,
        parameterId: data.parameterId,
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
    value: parameterData.value,
    parameterId: parameterData.parameterId,
  });

  const [loading, setLoading] = React.useState(true);
  const [parameterData, setParameterData] = React.useState({
    value: '',
    parameterId: '',
  });
  const [parameters, setParameters] = React.useState<Parameter[]>([]);
  const [open, setOpen] = React.useState<boolean>(false);
  const [message, setMessage] = React.useState<string>('');

  const handleValueChange = (event: React.ChangeEvent<{ value: unknown }>) => {
    const data = cloneData();
    data.value = event.target.value as string;
    setParameterData(data);
  };

  const handleParameterChange = (event: React.ChangeEvent<{ value: unknown }>) => {
    const data = cloneData();
    data.parameterId = event.target.value as string;
    setParameterData(data);
  };

  const handleClose = (event?: React.SyntheticEvent, reason?: string) => {
    if (reason === 'clickaway') {
      return;
    }

    setOpen(false);
  };

  const onClick = async () => {
    if (parameterData.value.length < 2) {
      setMessage('?????????????? ???????????????????? ????????????????');
      setOpen(true);
    } else if (parameterData.parameterId && parameterData.parameterId.length < 1) {
      setMessage('?????????????? ????????????????');
      setOpen(true);
    } else {
      const res = await updateParameterValue(props.id, parameterData.parameterId, parameterData.value);
      if (res && props.refresher) {
        props.refresher.refresh();
        props.setOpen(false);
      } else {
        setMessage('???? ?????????????? ?????????????????? ????????????');
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
            value={parameterData.value}
            onChange={handleValueChange}
            label="????????????????"
            variant="outlined"
          />
          <TextField
            id="parameter"
            select
            label="????????????????"
            className={classes.spaces}
            value={parameterData.parameterId}
            onChange={handleParameterChange}
            variant="outlined"
          >
            {parameters.map(p => (
              <MenuItem key={p.id} value={p.id}>
                {p.name}
              </MenuItem>
            ))}
          </TextField>
          <Grid container justifyContent="flex-end">
            <Button type="submit" className={classes.spaces} color="primary" variant="contained" onClick={onClick}>
              ????????????????
            </Button>
          </Grid>
        </React.Fragment>
      )}
    </Grid>
  );
};

export default EditParameterValue;
