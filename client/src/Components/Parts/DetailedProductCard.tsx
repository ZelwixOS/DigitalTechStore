import React from 'react';
import Card from '@material-ui/core/Card';
import Typography from '@material-ui/core/Typography';
import { createStyles, makeStyles, Theme } from '@material-ui/core/styles';
import Grid from '@material-ui/core/Grid';

import Product from 'src/Types/Product';
import PriceLikeBuyCard from 'src/Components/Parts/PriceLikeBuyCard';
import ParameterBlock from 'src/Types/ParameterBlock';

interface IDetailedProductCard {
  product?: Product;
  paramBlocks?: ParameterBlock[];
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
    props.paramBlocks?.map(block =>
      block.parameters.map(param => {
        if (param.important && string.length < 110) {
          string += `${param.name}:${param.value}, `;
        }
      }),
    );
    return `${string.substring(0, string.length - 2)}...`;
  };

  const strinTechParams = paramString();
  return (
    <Card variant="outlined" className={classes.card}>
      <Grid direction="row" justifyContent="center" container>
        <Grid item xs={12} sm={6} direction="column" justifyContent="center" alignItems="center" container>
          <img className={classes.image} src={props.image} />
          Артикул: {props.product?.vendorCode}
        </Grid>
        <Grid item xs={12} sm={6}>
          <Typography variant="h6" component="h6">
            {strinTechParams}
          </Typography>
          <PriceLikeBuyCard
            price={props.product?.price}
            priceWithoutDiscount={props.product?.priceWithoutDiscount}
            rating={props.product?.mark}
            id={props.product?.id}
            inCart={props.product?.inCart}
            inWishlist={props.product?.inWishlist}
            outlets={props.product?.outletProducts}
            isInWarehouse={props.product?.isInWarehouse}
          />
        </Grid>
      </Grid>
      <Grid direction="row" justifyContent="center" alignItems="center" container />
    </Card>
  );
};

export default DetailedProductCard;
