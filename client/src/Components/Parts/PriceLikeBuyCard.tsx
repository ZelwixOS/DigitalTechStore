import React, { useState } from 'react';
import Card from '@material-ui/core/Card';
import Typography from '@material-ui/core/Typography';
import { createStyles, makeStyles, Theme } from '@material-ui/core/styles';
import Rating from '@material-ui/lab/Rating';
import { Button } from '@material-ui/core';
import FavoriteIcon from '@material-ui/icons/Favorite';
import IconButton from '@material-ui/core/IconButton';
import Grid from '@material-ui/core/Grid';

import { addToCart, addToWishlist } from 'src/Requests/PostRequests';
import { deleteFromWishlist } from 'src/Requests/DeleteRequests';
import OutletProduct from 'src/Types/OutletProduct';

import UnitProducts from './UnitProducts';

interface IPriceLikeBuyCard {
  price?: number;
  id?: string;
  rating?: number;
  priceWithoutDiscount?: number;
  onBuy?: () => void;
  onWished?: () => void;
  inCart?: boolean;
  inWishlist?: boolean;
  outlets?: OutletProduct[];
  isInWarehouse?: boolean;
}

const useStyles = makeStyles((theme: Theme) =>
  createStyles({
    root: {
      display: 'flex',
      minHeight: 180,
      margin: theme.spacing(1),
    },
    grid: {
      minHeight: 80,
    },
    details: {
      display: 'flex',
      flexDirection: 'column',
    },
    content: {
      flex: '1 0 auto',
    },
    cover: {
      maxWidth: 151,
      maxHeight: 120,
      margin: theme.spacing(2),
    },
    bold: {
      fontWeight: 600,
    },
    thinbold: {
      fontWeight: 500,
    },
    crossed: {
      textDecoration: 'line-through',
    },
    toLeft: {
      paddingLeft: theme.spacing(1),
    },
    rating: {
      margin: theme.spacing(2),
    },
    ratingNum: {
      marginLeft: theme.spacing(2),
    },
  }),
);

const PriceLikeBuyCard: React.FC<IPriceLikeBuyCard> = props => {
  const checkInCart = () => {
    const role = sessionStorage.getItem('signed');
    if (!role) {
      const cart = localStorage.getItem('cartItems');
      if (cart) {
        return cart.includes(props.id as string);
      }
    }
    return props.inCart;
  };

  const [inCart, setInCart] = useState(checkInCart());
  const [inWishlist, setInWishlist] = useState(props.inWishlist);

  const addProductToCart = async () => {
    const role = sessionStorage.getItem('signed');

    if (role) {
      const response = await addToCart(props.id as string, 0);
      if (response !== null) {
        setInCart(true);
        if (props.onBuy) {
          props.onBuy();
        }
      }
    } else {
      const items = localStorage.getItem('cartItems');
      if (items) {
        if (items.includes(props.id as string)) {
          return;
        }
        localStorage.setItem('cartItems', `${items},${props.id}`);
      } else {
        localStorage.setItem('cartItems', props.id as string);
      }
      setInCart(true);
    }
  };

  const addProductToWishlist = async () => {
    const response = await addToWishlist(props.id as string);
    if (response !== null) {
      setInWishlist(true);
      if (props.onWished) {
        props.onWished();
      }
    }
  };

  const deleteProductFromWishlist = async () => {
    const response = await deleteFromWishlist(props.id as string);
    if (response === 1) {
      setInWishlist(false);
      if (props.onWished) {
        props.onWished();
      }
    }
  };

  const classes = useStyles();
  return (
    <Card variant="outlined" className={classes.root}>
      <Grid justify="center" alignItems="center" container>
        <Grid className={classes.grid} container direction="row" justify="center" alignItems="center">
          <Grid item container direction="column" justify="center" alignItems="center" xs={12} sm={6}>
            <Typography component="h5" variant="h5" className={classes.bold}>
              Цена:
            </Typography>
            {props.priceWithoutDiscount && props.price && props.priceWithoutDiscount > props.price ? (
              <Grid container justifyContent="center" direction="row">
                <Typography align="center" color="primary" component="h5" variant="h5" className={classes.bold}>
                  {props.price}₽
                </Typography>
                <Typography
                  align="center"
                  variant="h6"
                  color="textSecondary"
                  className={`${classes.crossed} ${classes.thinbold} ${classes.toLeft}`}
                >
                  {props.priceWithoutDiscount}
                </Typography>
              </Grid>
            ) : (
              <Typography align="center" component="h5" variant="h5" className={classes.bold}>
                {props.price}₽
              </Typography>
            )}
          </Grid>
          <Grid item xs={12} sm={6} container direction="row" justify="center" alignItems="center">
            <Grid item xs={12} sm={3} container justify="flex-start">
              {inWishlist ? (
                <IconButton aria-label="favourite" color="primary" onClick={deleteProductFromWishlist}>
                  <FavoriteIcon />
                </IconButton>
              ) : (
                <IconButton aria-label="favourite" onClick={addProductToWishlist}>
                  <FavoriteIcon />
                </IconButton>
              )}
            </Grid>
            <Grid item xs={12} sm={9}>
              {inCart ? (
                <Button color="primary" variant="outlined" href="/cart">
                  Корзина
                </Button>
              ) : (
                <Button color="primary" variant="contained" onClick={addProductToCart}>
                  Купить
                </Button>
              )}
            </Grid>
          </Grid>
        </Grid>
        <Grid container item xs={12} justify="center" alignContent="center" direction="row" className={classes.rating}>
          <Rating className={classes.rating} name="read-only" value={props.rating} readOnly />
          <Typography className={`${classes.bold} ${classes.rating}`}>{props.rating}</Typography>
        </Grid>
        <UnitProducts outlets={props.outlets} isInWarehouse={props.isInWarehouse} />
      </Grid>
    </Card>
  );
};

export default PriceLikeBuyCard;
