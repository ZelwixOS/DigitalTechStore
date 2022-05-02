import React from 'react';
import { createStyles, makeStyles, Theme } from '@material-ui/core/styles';
import Grid from '@material-ui/core/Grid';
import Typography from '@material-ui/core/Typography';

import OutletProduct from 'src/Types/OutletProduct';

import OutletInfo from './OutletInfo';

const useStyles = makeStyles((theme: Theme) =>
  createStyles({
    bar: {
      backgroundColor: 'white',
    },
  }),
);

interface IOutletCount {
  outlet: OutletProduct;
}

const OutletCount: React.FC<IOutletCount> = props => {
  const classes = useStyles();

  return (
    <Grid item xs={12}>
      <Grid direction="row" justify="space-evenly" alignItems="center" item xs={12} container>
        <Grid item xs={12} sm={10}>
          <OutletInfo outlet={props.outlet.outlet} />
        </Grid>
        <Typography>{props.outlet.count}</Typography>
      </Grid>
    </Grid>
  );
};
export default OutletCount;
