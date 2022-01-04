import React from 'react';
import Button from '@material-ui/core/Button';
import { makeStyles, createStyles, Theme } from '@material-ui/core/styles';
import {
  Dialog,
  DialogActions,
  DialogContent,
  DialogContentText,
  DialogTitle,
  Grid,
  Link,
  Typography,
} from '@material-ui/core';

import LoginForm from './LoginForm';

const useStyles = makeStyles((theme: Theme) =>
  createStyles({
    buttons: {
      margin: theme.spacing(2),
    },
  }),
);

const LoginModal: React.FC = () => {
  const classes = useStyles();
  const [open, setOpen] = React.useState(false);

  const handleClickOpen = () => {
    setOpen(true);
  };

  const handleClose = () => {
    setOpen(false);
  };

  return (
    <div>
      <Button className={classes.buttons} color="secondary" variant="outlined" onClick={handleClickOpen}>
        LogIn
      </Button>
      <Dialog
        open={open}
        onClose={handleClose}
        aria-labelledby="alert-dialog-title"
        aria-describedby="alert-dialog-description"
      >
        <DialogTitle id="alert-dialog-title">Sign in</DialogTitle>
        <DialogContent>
          <DialogContentText id="alert-dialog-description">
            <LoginForm />
          </DialogContentText>
        </DialogContent>
        <DialogActions>
          <Grid container justify="center">
            <Typography>You are new here?</Typography>
            <Link href="/Registration"> Regiser </Link>
          </Grid>
        </DialogActions>
      </Dialog>
    </div>
  );
};

export default LoginModal;
