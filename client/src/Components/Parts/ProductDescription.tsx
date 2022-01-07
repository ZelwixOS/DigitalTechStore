import React from 'react';
import Typography from '@material-ui/core/Typography';
import { createStyles, makeStyles, Theme } from '@material-ui/core/styles';
import Grid from '@material-ui/core/Grid';

interface IProductDescription {
  productName: string;
  productDescription: string;
}

const useStyles = makeStyles((theme: Theme) =>
  createStyles({
    prodName: {
      fontSize: 1.3 * theme.typography.fontSize,
      fontWeight: theme.typography.fontWeightBold,
    },
    descriptionText: {
      margin: theme.spacing(2),
    },
  }),
);

const ProductDescription: React.FC<IProductDescription> = props => {
  const classes = useStyles();
  return (
    <Grid container direction="column" alignItems="center" justify="center">
      <Typography className={classes.prodName} variant="overline">
        {`Описание ${props.productName}`}
      </Typography>
      <Grid className={classes.descriptionText} container justify="flex-start">
        <Typography variant="body2">{props.productDescription}</Typography>
      </Grid>
    </Grid>
  );
};

export default ProductDescription;
