import React from 'react';
import AppBar from '@material-ui/core/AppBar';
import Toolbar from '@material-ui/core/Toolbar';
import { createStyles, makeStyles, Theme } from '@material-ui/core/styles';
import Button from '@material-ui/core/Button';
import FavoriteBorderIcon from '@material-ui/icons/FavoriteBorder';
import ShoppingCartOutlinedIcon from '@material-ui/icons/ShoppingCartOutlined';
import SearchIcon from '@material-ui/icons/Search';
import InputBase from '@material-ui/core/InputBase';
import Grid from '@material-ui/core/Grid';
import ButtonGroup from '@material-ui/core/ButtonGroup';
import DevicesIcon from '@material-ui/icons/Devices';

import CategorySelector from './CategorySelector';

const useStyles = makeStyles((theme: Theme) =>
  createStyles({
    buttons: {
      margin: theme.spacing(2),
    },
    search: {
      position: 'relative',
      borderRadius: theme.shape.borderRadius,
      backgroundColor: theme.palette.common.white,
      marginRight: theme.spacing(2),
      marginLeft: 0,
      width: '100%',
      [theme.breakpoints.up('sm')]: {
        marginLeft: theme.spacing(3),
        width: 'auto',
      },
    },
    inputRoot: {
      color: '#000',
    },
    inputInput: {
      padding: theme.spacing(1, 1, 1, 0),
      // vertical padding + font size from searchIcon
      paddingLeft: `calc(1em + ${theme.spacing(4)}px)`,
      transition: theme.transitions.create('width'),
      width: '100%',
      [theme.breakpoints.up('md')]: {
        width: '20ch',
      },
    },
    searchIcon: {
      color: '#999',
      padding: theme.spacing(0, 2),
      height: '100%',
      position: 'absolute',
      pointerEvents: 'none',
      display: 'flex',
      alignItems: 'center',
      justifyContent: 'center',
    },
    logoButton: {
      fontSize: '30px',
      fontFamily: 'cursive',
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
            <Button
              variant="text"
              size="large"
              color="secondary"
              href="/"
              className={classes.logoButton}
              startIcon={<DevicesIcon />}
            >
              DDoS
            </Button>
          </Grid>
          <Grid item xs={12} sm={2}>
            <CategorySelector />
          </Grid>
          <Grid item xs={12} sm={3}>
            <div className={classes.search}>
              <div className={classes.searchIcon}>
                <SearchIcon />
              </div>
              <InputBase
                placeholder="Search"
                classes={{
                  root: classes.inputRoot,
                  input: classes.inputInput,
                }}
                inputProps={{ 'aria-label': 'search' }}
              />
            </div>
          </Grid>
          <Grid item xs={12} sm={3}>
            <ButtonGroup variant="outlined" color="secondary" size="large" aria-label="text primary button group">
              <Button startIcon={<FavoriteBorderIcon />}>Favorite</Button>
              <Button startIcon={<ShoppingCartOutlinedIcon />}>Cart</Button>
            </ButtonGroup>
          </Grid>
          <Grid item xs={12} sm={2}>
            <Button className={classes.buttons} color="secondary" variant="outlined">
              LogIn
            </Button>
          </Grid>
        </Toolbar>
      </AppBar>
      <Toolbar />
    </React.Fragment>
  );
};
export default NavigationBar;
