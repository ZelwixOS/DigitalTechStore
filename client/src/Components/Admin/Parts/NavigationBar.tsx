import React, { useEffect, useState } from 'react';
import AppBar from '@material-ui/core/AppBar';
import Toolbar from '@material-ui/core/Toolbar';
import { createStyles, makeStyles, Theme } from '@material-ui/core/styles';
import Button from '@material-ui/core/Button';
import Grid from '@material-ui/core/Grid';
import DevicesIcon from '@material-ui/icons/Devices';

import HiddenNavigation from './HiddenNavigation';

const useStyles = makeStyles((theme: Theme) =>
  createStyles({
    logoButton: {
      fontSize: '30px',
      fontFamily: 'cursive',
      [theme.breakpoints.down('sm')]: {
        display: 'none',
      },
    },
    logoIcon: {
      [theme.breakpoints.up('md')]: {
        display: 'none',
      },
    },
  }),
);

const NavigationBar: React.FC = () => {
  const classes = useStyles();

  return (
    <React.Fragment>
      <AppBar>
        <Toolbar>
          <Grid item xs={12} sm={2}>
            <HiddenNavigation />
          </Grid>
          <Grid container justifyContent="flex-end" item xs={12} sm={10}>
            <Button
              variant="text"
              size="large"
              color="secondary"
              href="/"
              className={classes.logoButton}
              startIcon={<DevicesIcon />}
            >
              DTS
            </Button>
            <Button variant="text" size="large" color="secondary" href="/" className={classes.logoIcon}>
              <DevicesIcon />
            </Button>
          </Grid>
        </Toolbar>
      </AppBar>
      <Toolbar />
    </React.Fragment>
  );
};
export default NavigationBar;
