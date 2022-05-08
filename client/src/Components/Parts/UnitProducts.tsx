import React, { useState } from 'react';
import { createStyles, makeStyles } from '@material-ui/core/styles';
import Button from '@material-ui/core/Button';
import Grid from '@material-ui/core/Grid';
import Dialog from '@material-ui/core/Dialog';
import DialogTitle from '@material-ui/core/DialogTitle';
import DialogContent from '@material-ui/core/DialogContent';
import { Typography } from '@material-ui/core';

import OutletProduct from 'src/Types/OutletProduct';

import OutletCount from './OutletCount';

const useStyles = makeStyles(() =>
  createStyles({
    bar: {
      backgroundColor: 'white',
    },
    dialog: {
      minWidth: 480,
    },
  }),
);

interface ICityNavigation {
  outlets?: OutletProduct[];
  isInWarehouse?: boolean;
}

const CityNavigation: React.FC<ICityNavigation> = props => {
  const classes = useStyles();

  const [open, setOpen] = useState(false);

  const handleClose = () => {
    setOpen(false);
  };

  const handleClickOpen = () => {
    setOpen(true);
  };

  return (
    <Grid item xs={12}>
      <Grid direction="row" justify="space-evenly" alignItems="stretch" item xs={12} container>
        <Button variant="text" size="small" color="primary" onClick={handleClickOpen}>
          В наличии в магазинах: {props.outlets?.length}
        </Button>
        <Typography>На складе: {props.isInWarehouse ? 'есть' : 'нет'}</Typography>
      </Grid>
      <Dialog
        open={open}
        onClose={handleClose}
        aria-labelledby="alert-dialog-title"
        aria-describedby="alert-dialog-description"
        className={classes.dialog}
      >
        <DialogTitle id="alert-dialog-title">В магазинах:</DialogTitle>
        <DialogContent className={classes.dialog}>
          {props.outlets?.map((o, index) => (
            <OutletCount outlet={o} key={index} />
          ))}
        </DialogContent>
      </Dialog>
    </Grid>
  );
};
export default CityNavigation;
