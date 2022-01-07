import React from 'react';
import Typography from '@material-ui/core/Typography';
import { createStyles, makeStyles, Theme } from '@material-ui/core/styles';
import Grid from '@material-ui/core/Grid';

import Parameter from 'src/Types/Parameter';
import ProductParameter from 'src/Components/Parts/ProductParameter';

interface IProductParams {
  productName: string;
  params: Parameter[];
}

const useStyles = makeStyles((theme: Theme) =>
  createStyles({
    prodName: {
      fontSize: 1.3 * theme.typography.fontSize,
      fontWeight: theme.typography.fontWeightBold,
    },
  }),
);

const ProductParams: React.FC<IProductParams> = props => {
  const classes = useStyles();
  return (
    <Grid container direction="column" alignItems="center" justify="center">
      <Typography className={classes.prodName} variant="overline">
        {`Характеристики ${props.productName}`}
      </Typography>
      {props.params?.map((param, index) => (
        <ProductParameter param={param} key={index} />
      ))}
    </Grid>
  );
};

export default ProductParams;
