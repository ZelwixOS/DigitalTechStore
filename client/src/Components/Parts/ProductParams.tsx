import React from 'react';
import Typography from '@material-ui/core/Typography';
import { createStyles, makeStyles, Theme } from '@material-ui/core/styles';
import Grid from '@material-ui/core/Grid';

import ParameterBlock from 'src/Types/ParameterBlock';
import ProductParameter from 'src/Components/Parts/ProductParameter';

interface IProductParams {
  productName: string;
  params: ParameterBlock[];
}

const useStyles = makeStyles((theme: Theme) =>
  createStyles({
    prodName: {
      fontSize: 1.3 * theme.typography.fontSize,
      fontWeight: theme.typography.fontWeightBold,
    },
    line: {
      fontSize: 1.1 * theme.typography.fontSize,
      fontWeight: theme.typography.fontWeightBold,
    },
  }),
);

const ProductParams: React.FC<IProductParams> = props => {
  const classes = useStyles();
  return (
    <Grid container direction="column" alignItems="center" justify="space-around" item xs={12}>
      <Typography className={classes.prodName} variant="overline">
        {`Характеристики ${props.productName}`}
      </Typography>
      {props.params?.map((block, index) => (
        <Grid direction="column" alignItems="center" container key={index}>
          <Grid item xs={12} sm={6} container alignItems="center" justify="space-evenly" className={classes.line}>
            {block.name}
          </Grid>
          {block.parameters.map((param, key) => (
            <ProductParameter param={param} key={key} />
          ))}
        </Grid>
      ))}
    </Grid>
  );
};

export default ProductParams;
