import React from 'react';
import Grid from '@material-ui/core/Grid';
import Link from '@material-ui/core/Link';
import { Button, createStyles, makeStyles, Theme, Typography } from '@material-ui/core';
import DateFnsUtils from '@date-io/date-fns';
import { KeyboardDatePicker } from '@material-ui/pickers';
import { MuiPickersUtilsProvider } from '@material-ui/pickers';

import { getStatFile } from 'src/Requests/GetRequests';

const useStyles = makeStyles((theme: Theme) =>
  createStyles({
    root: {
      marginTop: theme.spacing(10),
      color: '#CCC',
      backgroundColor: '#444',
    },
  }),
);

const CreateFileReport = () => {
  const classes = useStyles();

  const now = new Date();
  const monthBegginig = new Date(now.getFullYear(), now.getMonth(), 1);
  const monthEnd = new Date(now.getFullYear(), now.getMonth() + 1, 1);
  const [selectedFromDate, setFromSelectedDate] = React.useState<Date>(monthBegginig);
  const [selectedToDate, setToSelectedDate] = React.useState<Date>(monthEnd);

  const handleFromDateChange = (date: Date | null) => {
    setFromSelectedDate(date as Date);
  };

  const handleToDateChange = (date: Date | null) => {
    setToSelectedDate(date as Date);
  };

  const onClick = async () => {
    const res = await getStatFile(selectedFromDate, selectedToDate);
    if (res) {
      const link = document.createElement('a');
      link.download = '';
      link.href = `https://localhost:5001${res}`;
      document.body.appendChild(link);
      link.click();
      document.body.removeChild(link);
    }
  };

  return (
    <MuiPickersUtilsProvider utils={DateFnsUtils}>
      <Grid container direction="row">
        <Grid item xs={12} sm={4} container justifyContent="flex-end">
          <KeyboardDatePicker
            margin="normal"
            id="date-picker-dialog-from"
            label="От"
            format="dd.MM.yyyy"
            value={selectedFromDate}
            onChange={handleFromDateChange}
            KeyboardButtonProps={{
              'aria-label': 'Изменить дату',
            }}
          />
        </Grid>
        <Grid item xs={12} sm={4} container justifyContent="center">
          <KeyboardDatePicker
            margin="normal"
            id="date-picker-dialog-to"
            label="До"
            format="dd.MM.yyyy"
            value={selectedToDate}
            onChange={handleToDateChange}
            KeyboardButtonProps={{
              'aria-label': 'Изменить дату',
            }}
          />
        </Grid>
        <Grid item xs={12} sm={4} container justifyContent="flex-start" alignContent="center">
          <Button variant="contained" color="primary" onClick={onClick}>
            Получить отчёт
          </Button>
        </Grid>
      </Grid>
    </MuiPickersUtilsProvider>
  );
};

export default CreateFileReport;
