import React, { useState, useEffect } from 'react';
import Card from '@material-ui/core/Card';
import CardContent from '@material-ui/core/CardContent';
import Typography from '@material-ui/core/Typography';
import { createStyles, makeStyles, Theme } from '@material-ui/core/styles';
import Rating from '@material-ui/lab/Rating';
import { Button } from '@material-ui/core';
import FavoriteIcon from '@material-ui/icons/Favorite';
import IconButton from '@material-ui/core/IconButton';
import Grid from '@material-ui/core/Grid';
import Link from '@material-ui/core/Link';

import Product from 'src/Types/Product';

interface IProductCard {
  product: Product;
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
    cover: {
      maxWidth: 151,
      maxHeight: 120,
      margin: theme.spacing(1),
    },
    bold: {
      fontWeight: 600,
    },
    rating: {
      margin: theme.spacing(1),
    },
  }),
);

const ProductCard: React.FC<IProductCard> = props => {
  const [isShown, setIsShown] = useState(false);
  const picUrl = 'https://localhost:5001/products/';
  const [picture, setPicture] = useState(`${picUrl}${props.product.picURL}`);
  const classes = useStyles();

  useEffect(() => {
    let isMounted = true;
    const img = new Image();
    img.src = `${picUrl}${props.product.picURL}`;

    if (isMounted) {
      img.onerror = () => setPicture(`${picUrl}noPic.jpg`);
    }

    return () => {
      isMounted = false;
    };
  });

  return (
    <Card
      variant="outlined"
      className={classes.root}
      onMouseEnter={() => setIsShown(true)}
      onMouseLeave={() => setIsShown(false)}
    >
      <Grid container direction="column" alignItems="center" justify="center" xs={12} sm={3}>
        <img className={classes.cover} src={picture} alt={props.product.name} />
        <Typography variant="body1">{isShown && props.product.vendorCode}</Typography>
      </Grid>
      <Grid item xs={12} sm={6} container direction="column">
        <CardContent className={classes.content}>
          <Grid>
            <Typography component="h5" variant="h5">
              <Link href={`/product/${props.product.id}`} color="inherit">
                {props.product.name}
              </Link>
            </Typography>
            <Typography component="h5" variant="h5" className={classes.bold}>
              {props.product.price}₽
            </Typography>
          </Grid>
        </CardContent>
        <Grid className={classes.rating}>
          <Rating name="read-only" value={props.product.mark} readOnly />
        </Grid>
      </Grid>
      <Grid container direction="row" justify="center" alignItems="center" item xs={12} sm={3}>
        <Grid item xs={12} sm={4}>
          <IconButton aria-label="favourite" className={classes.button}>
            <FavoriteIcon />
          </IconButton>
        </Grid>
        <Grid item xs={12} sm={8}>
          <Button className={classes.button} variant="outlined">
            Купить
          </Button>
        </Grid>
      </Grid>
    </Card>
  );
};

export default ProductCard;
