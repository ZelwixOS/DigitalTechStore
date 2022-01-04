import React from 'react';
import Typography from '@material-ui/core/Typography';
import { createStyles, makeStyles, Theme } from '@material-ui/core/styles';
import Grid from '@material-ui/core/Grid';

import Parameter from 'src/Types/Parameter';

interface IProductParameter {
  param: Parameter;
}

const useStyles = makeStyles((theme: Theme) =>
  createStyles({
    line: {
      borderBottom: `1px solid ${theme.palette.divider}`,
      padding: theme.spacing(1, 0, 1, 0),
    },
  }),
);

const ProductParameter: React.FC<IProductParameter> = props => {
  const classes = useStyles();
  return (
    <Grid direction="row" container alignItems="center" justify="center" className={classes.line}>
      <Grid item xs={12} sm={6}>
        <Typography variant="body2">{props.param.name}</Typography>
      </Grid>
      <Grid item xs={12} sm={6}>
        <Typography variant="body2">{props.param.value}</Typography>
      </Grid>
    </Grid>
  );
};

export default ProductParameter;
