import React, { useState, useEffect } from 'react';
import Card from '@material-ui/core/Card';
import CardContent from '@material-ui/core/CardContent';
import Typography from '@material-ui/core/Typography';
import { createStyles, makeStyles, Theme } from '@material-ui/core/styles';
import Rating from '@material-ui/lab/Rating';
import { Button, Snackbar } from '@material-ui/core';
import FavoriteIcon from '@material-ui/icons/Favorite';
import Close from '@mui/icons-material/Close';
import IconButton from '@material-ui/core/IconButton';
import Grid from '@material-ui/core/Grid';
import Link from '@material-ui/core/Link';
import Checkbox from '@material-ui/core/Checkbox';
import { Alert } from '@material-ui/lab';

import Product from 'src/Types/Product';
import { addToCart, addToWishlist } from 'src/Requests/PostRequests';
import { deleteFromWishlist } from 'src/Requests/DeleteRequests';

import PoductCounter from './PoductCounter';

interface IProductCard {
  product: Product;
  hideBuy?: boolean;
  hideLike?: boolean;
  showCounter?: boolean;
  count?: number;
  isChecked?: boolean;
  onCount?: (newCount: number, productId?: string) => void;
  onDelete?: (productId: string) => void;
  onBuy?: () => void;
  onWished?: () => void;
  onChecked?: (product: Product, added: boolean) => void;
}

const useStyles = makeStyles((theme: Theme) =>
  createStyles({
    root: {
      display: 'flex',
      minHeight: 180,
      marginBottom: theme.spacing(1),
    },
    details: {
      display: 'flex',
      flexDirection: 'column',
    },
    content: {
      flex: '1 0 auto',
    },
    button: {
      padding: theme.spacing(1),
    },
    buttonright: {
      paddingLeft: theme.spacing(1),
      paddingTop: theme.spacing(1),
      paddingBottom: theme.spacing(1),
    },
    cover: {
      maxWidth: 151,
      maxHeight: 120,
      margin: theme.spacing(1),
    },
    bold: {
      fontWeight: 600,
    },
    thinbold: {
      fontWeight: 550,
    },
    crossed: {
      textDecoration: 'line-through',
    },
    rating: {
      margin: theme.spacing(1),
    },
    ratingNum: {
      marginLeft: theme.spacing(1),
    },
  }),
);

const ProductCard: React.FC<IProductCard> = props => {
  const role = sessionStorage.getItem('signed');
  const [isShown, setIsShown] = useState(false);
  const [isChecked, setChecked] = useState(props.isChecked ?? false);
  const picUrl = 'https://localhost:5001/products/';
  const [picture, setPicture] = useState(`${picUrl}${props.product.picURL}`);
  const classes = useStyles();

  useEffect(() => {
    let isMounted = true;
    const img = new Image();
    img.src = `${picUrl}${props.product.picURL}`;

    if (!role || role === 'Guest') {
      const cart = localStorage.getItem('cartItems');
      if (cart) {
        props.product.inCart = cart.includes(props.product.id);
        setInCart(props.product.inCart);
      }
    }

    if (isMounted) {
      img.onerror = () => setPicture(`${picUrl}noPic.jpg`);
    }

    return () => {
      isMounted = false;
    };
  });

  const [inCart, setInCart] = useState(props.product.inCart);
  const [inWishlist, setInWishlist] = useState(props.product.inWishlist);

  const addProductToCart = async () => {
    if (role && role !== 'Guest') {
      const response = await addToCart(props.product.id, 0);
      if (response !== null) {
        setInCart(true);
        if (props.onBuy) {
          props.onBuy();
        }
      }
    } else {
      const items = localStorage.getItem('cartItems');

      if (items) {
        if (items.includes(props.product.id)) {
          return;
        }
        localStorage.setItem('cartItems', `${items},${props.product.id}`);
      } else {
        localStorage.setItem('cartItems', props.product.id);
      }
    }
  };

  const addProductToWishlist = async () => {
    if (role) {
      const response = await addToWishlist(props.product.id);
      if (response !== null) {
        setInWishlist(true);
        if (props.onWished) {
          props.onWished();
        }
      }
    } else {
      setOpen(true);
    }
  };

  const deleteProductFromWishlist = async () => {
    const response = await deleteFromWishlist(props.product.id);
    if (response === 1) {
      setInWishlist(false);
      if (props.onWished) {
        props.onWished();
      }
    }
  };

  const onDelete = props.onDelete?.bind(this, props.product.id);

  const onChange = () => {
    if (props.onChecked) {
      props.onChecked(props.product, !isChecked);
    }

    setChecked(!isChecked);
  };

  const [open, setOpen] = React.useState(false);

  const handleClose = (event?: React.SyntheticEvent, reason?: string) => {
    if (reason === 'clickaway') {
      return;
    }

    setOpen(false);
  };

  return (
    <Card
      variant="outlined"
      className={classes.root}
      onMouseEnter={() => setIsShown(true)}
      onMouseLeave={() => setIsShown(false)}
    >
      <Snackbar
        anchorOrigin={{ vertical: 'top', horizontal: 'center' }}
        open={open}
        autoHideDuration={6000}
        onClose={handleClose}
      >
        <Alert onClose={handleClose} severity="info">
          Зарегистрируйтесь для использования всех возможностей магазина!
        </Alert>
      </Snackbar>
      <Grid container direction="row" alignItems="center" justifyContent="center">
        {props.onChecked && (
          <Grid container direction="column" item alignItems="center" justifyContent="center" xs={12} sm={1}>
            <Checkbox color="primary" onChange={onChange} checked={isChecked} />
          </Grid>
        )}
        <Grid container direction="column" alignItems="center" justifyContent="center" item xs={12} sm={3}>
          <img className={classes.cover} src={picture} alt={props.product.name} />
          <Typography variant="body1">{isShown && props.product.vendorCode}</Typography>
        </Grid>
        <Grid item xs={12} sm={6} container direction="column">
          <CardContent className={classes.content}>
            <Grid>
              <Typography align="center" component="h5" variant="h5">
                <Link href={`/product/${props.product.id}`} color="inherit">
                  {props.product.name}
                </Link>
              </Typography>
              {props.product.priceWithoutDiscount && props.product.priceWithoutDiscount > props.product.price ? (
                <React.Fragment>
                  <Typography
                    align="center"
                    variant="h6"
                    color="textSecondary"
                    className={`${classes.crossed} ${classes.thinbold}`}
                  >
                    {props.product.priceWithoutDiscount}₽
                  </Typography>
                  <Typography align="center" color="primary" component="h5" variant="h5" className={classes.bold}>
                    {props.product.price}₽
                  </Typography>
                </React.Fragment>
              ) : (
                <Typography align="center" component="h5" variant="h5" className={classes.bold}>
                  {props.product.price}₽
                </Typography>
              )}
            </Grid>
          </CardContent>
          <Grid
            container
            item
            xs={12}
            justifyContent="center"
            alignContent="center"
            direction="row"
            className={classes.rating}
          >
            <Rating name="read-only" value={props.product.mark} readOnly />
            <Typography className={`${classes.bold} ${classes.ratingNum}`}>{props.product.mark}</Typography>
          </Grid>
        </Grid>
        <Grid container direction="row" justifyContent="center" alignItems="center" item xs={12} sm={2}>
          {!props.hideLike && (
            <Grid container justifyContent="center" item xs={12} sm={4}>
              {inWishlist ? (
                <IconButton
                  aria-label="favourite"
                  color="primary"
                  className={classes.button}
                  onClick={deleteProductFromWishlist}
                >
                  <FavoriteIcon />
                </IconButton>
              ) : (
                <IconButton aria-label="favourite" className={classes.button} onClick={addProductToWishlist}>
                  <FavoriteIcon />
                </IconButton>
              )}
            </Grid>
          )}
          {!props.hideBuy && (
            <Grid container justifyContent="center" item xs={12} sm={8}>
              {inCart ? (
                <Button className={classes.button} variant="contained" color="primary" href="/cart">
                  В Корзине
                </Button>
              ) : (
                <Button className={classes.button} variant="outlined" onClick={addProductToCart}>
                  Купить
                </Button>
              )}
            </Grid>
          )}
          {props.showCounter && (
            <Grid item xs={12} sm={6}>
              <PoductCounter id={props.product.id} count={props.count as number} onCount={props.onCount} />
            </Grid>
          )}
          {props.onDelete && (
            <Grid item xs={12} sm={4}>
              <IconButton aria-label="favourite" className={classes.buttonright} onClick={onDelete}>
                <Close />
              </IconButton>
            </Grid>
          )}
        </Grid>
      </Grid>
    </Card>
  );
};

export default ProductCard;
