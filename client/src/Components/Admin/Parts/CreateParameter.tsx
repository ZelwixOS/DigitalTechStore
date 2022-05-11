import React, { useEffect } from 'react';
import Button from '@material-ui/core/Button';
import { createStyles, makeStyles, Theme } from '@material-ui/core/styles';
import { Checkbox, CircularProgress, FormControlLabel, Grid, MenuItem, Snackbar, TextField } from '@material-ui/core';
import { Alert } from '@mui/material';

import { createParameter } from 'src/Requests/PostRequests';
import ParameterBlock from 'src/Types/ParameterBlock';
import { getParamBlocks } from 'src/Requests/GetRequests';

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

interface ICreateParameter {
  refresher?: IRefresher;
  setOpen: React.Dispatch<React.SetStateAction<boolean>>;
}

const CreateParameter: React.FC<ICreateParameter> = props => {
  const classes = useStyles();

  const getData = async (isMounted: boolean) => {
    setLoading(true);
    const res = await getParamBlocks();
    if (isMounted) {
      setParameterBlocks(res);
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
    important: parameterData.important,
    parameterBlockId: parameterData.parameterBlockId,
    range: parameterData.range,
    minValue: parameterData.minValue,
    maxValue: parameterData.maxValue,
  });

  const [loading, setLoading] = React.useState(true);
  const [parameterData, setParameterData] = React.useState({
    name: '',
    important: true,
    parameterBlockId: '',
    range: true,
    minValue: 0,
    maxValue: 0,
  });
  const [parameterBlocks, setParameterBlocks] = React.useState<ParameterBlock[]>([]);
  const [open, setOpen] = React.useState<boolean>(false);
  const [message, setMessage] = React.useState<string>('');

  const handleNameChange = (event: React.ChangeEvent<{ value: unknown }>) => {
    const data = cloneData();
    data.name = event.target.value as string;
    setParameterData(data);
  };

  const handleMinValueChange = (event: React.ChangeEvent<{ value: unknown }>) => {
    const data = cloneData();
    data.minValue = event.target.value as number;
    setParameterData(data);
  };

  const handleMaxValueChange = (event: React.ChangeEvent<{ value: unknown }>) => {
    const data = cloneData();
    data.maxValue = event.target.value as number;
    setParameterData(data);
  };

  const handleRangeChange = (event: React.ChangeEvent<HTMLInputElement>, checked: boolean) => {
    const data = cloneData();
    data.range = checked;
    setParameterData(data);
  };

  const handleImportantChange = (event: React.ChangeEvent<HTMLInputElement>, checked: boolean) => {
    const data = cloneData();
    data.important = checked;
    setParameterData(data);
  };

  const handleBlockChange = (event: React.ChangeEvent<{ value: unknown }>) => {
    const data = cloneData();
    data.parameterBlockId = event.target.value as string;
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
    } else if (parameterData.range && parameterData.minValue / 1 >= parameterData.maxValue / 1) {
      setMessage('Введите корректные значения минимума и максимума');
      setOpen(true);
    } else {
      const res = await createParameter(
        parameterData.name,
        parameterData.important,
        parameterData.range,
        parameterData.minValue,
        parameterData.maxValue,
        parameterData.parameterBlockId,
      );
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
            label="Название"
            variant="outlined"
          />
          <FormControlLabel
            control={
              <Checkbox
                checked={parameterData.important}
                onChange={handleImportantChange}
                name="important"
                color="primary"
              />
            }
            label="Важный"
            className={classes.spaces}
          />
          <TextField
            id="parameterBlock"
            select
            label="Блок параметров"
            className={classes.spaces}
            value={parameterData.parameterBlockId}
            onChange={handleBlockChange}
            variant="outlined"
          >
            {parameterBlocks.map(p => (
              <MenuItem key={p.id} value={p.id}>
                {p.name}
              </MenuItem>
            ))}
          </TextField>
          <FormControlLabel
            control={
              <Checkbox checked={parameterData.range} onChange={handleRangeChange} name="range" color="primary" />
            }
            label="Диапазон"
            className={classes.spaces}
          />
          {parameterData.range && (
            <Grid container direction="row">
              <TextField
                id="parameterMin"
                className={classes.spaces}
                value={parameterData.minValue}
                onChange={handleMinValueChange}
                label="Минимум"
                variant="outlined"
                type="number"
              />
              <TextField
                id="parameterMax"
                className={classes.spaces}
                value={parameterData.maxValue}
                onChange={handleMaxValueChange}
                label="Максимум"
                variant="outlined"
                type="number"
              />
            </Grid>
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

export default CreateParameter;
