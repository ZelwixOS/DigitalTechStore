import React, { useEffect, useState } from 'react';
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

import { getRole } from 'src/Requests/AccountRequests';
import Roles from 'src/Types/Roles';

import CategorySelector from './CategorySelector';
import LoginModal from './LoginModal';
import UserMiniPanel from './UserMiniPanel';

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
      [theme.breakpoints.down('sm')]: {
        display: 'none',
      },
    },
    logoIcon: {
      [theme.breakpoints.up('md')]: {
        display: 'none',
      },
    },
    largeObject: {
      [theme.breakpoints.down('md')]: {
        display: 'none',
      },
    },
    smallObject: {
      [theme.breakpoints.up('lg')]: {
        display: 'none',
      },
    },
  }),
);

const NavigationBar: React.FC = () => {
  const classes = useStyles();

  const [isAuth, setAuth] = useState<boolean>(false);
  const [loaded, setLoaded] = useState<boolean>(false);

  useEffect(() => {
    let isMounted = true;
    checkAuth(isMounted);
    return () => {
      isMounted = false;
    };
  });

  const checkAuth = async (isMounted: boolean) => {
    const authres = await getRole();

    if (isMounted) {
      if (authres !== Roles.guest) {
        setAuth(true);
      } else {
        setAuth(false);
      }
      setLoaded(true);
    }
  };

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
              DTS
            </Button>
            <Button variant="text" size="large" color="secondary" href="/" className={classes.logoIcon}>
              <DevicesIcon />
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
                placeholder="Поиск"
                classes={{
                  root: classes.inputRoot,
                  input: classes.inputInput,
                }}
                inputProps={{ 'aria-label': 'search' }}
              />
            </div>
          </Grid>
          <Grid item xs={12} sm={3}>
            <ButtonGroup
              className={classes.smallObject}
              variant="outlined"
              color="secondary"
              size="large"
              aria-label="text primary button group"
            >
              <Button href="/wishlist">
                <FavoriteBorderIcon />
              </Button>
              <Button href="/cart">
                <ShoppingCartOutlinedIcon />
              </Button>
            </ButtonGroup>
            <ButtonGroup
              className={classes.largeObject}
              variant="outlined"
              color="secondary"
              size="large"
              aria-label="text primary button group"
            >
              <Button href="/wishlist" startIcon={<FavoriteBorderIcon />}>
                Избранное
              </Button>
              <Button href="/cart" startIcon={<ShoppingCartOutlinedIcon />}>
                Корзина
              </Button>
            </ButtonGroup>
          </Grid>
          <Grid item xs={12} sm={2}>
            {loaded && (isAuth ? <UserMiniPanel /> : <LoginModal />)}
          </Grid>
        </Toolbar>
      </AppBar>
      <Toolbar />
    </React.Fragment>
  );
};
export default NavigationBar;
