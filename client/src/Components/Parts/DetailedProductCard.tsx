import React from 'react';
import Card from '@material-ui/core/Card';
import Typography from '@material-ui/core/Typography';
import { createStyles, makeStyles, Theme } from '@material-ui/core/styles';
import Grid from '@material-ui/core/Grid';

import Product from 'src/Types/Product';
import PriceLikeBuyCard from 'src/Components/Parts/PriceLikeBuyCard';

interface IDetailedProductCard {
  product?: Product;
  image?: string;
}

const useStyles = makeStyles((theme: Theme) =>
  createStyles({
    image: {
      maxWidth: 260,
      maxHeight: 400,
      margin: theme.spacing(2),
    },
    card: {
      padding: theme.spacing(2),
    },
  }),
);

const DetailedProductCard: React.FC<IDetailedProductCard> = props => {
  const classes = useStyles();
  const paramString = (): string => {
    let string = '';
    props.product?.productParameter.map(param => {
      if (param.important) {
        string += `${param.name}:${param.value}, `;
      }
    });
    return string.substring(0, string.length - 2);
  };

  const strinTechParams = paramString();
  return (
    <Card variant="outlined" className={classes.card}>
      <Grid direction="row" justify="center" container>
        <Grid item xs={12} sm={6} direction="column" justify="center" alignItems="center" container>
          <img className={classes.image} src={props.image} />
          Артикул: {props.product?.vendorCode}
        </Grid>
        <Grid item xs={12} sm={6}>
          <Typography variant="h6" component="h6">
            {strinTechParams}
          </Typography>
          <PriceLikeBuyCard price={props.product?.price} rating={props.product?.mark} id={props.product?.id} />
        </Grid>
      </Grid>
      <Grid direction="row" justify="center" alignItems="center" container />
    </Card>
  );
};

export default DetailedProductCard;
