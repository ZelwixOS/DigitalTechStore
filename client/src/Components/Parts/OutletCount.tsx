import React from 'react';
import Grid from '@material-ui/core/Grid';
import Typography from '@material-ui/core/Typography';

import OutletProduct from 'src/Types/OutletProduct';

import OutletInfo from './OutletInfo';

interface IOutletCount {
  outlet: OutletProduct;
}

const OutletCount: React.FC<IOutletCount> = props => (
  <Grid item xs={12}>
    <Grid direction="row" justifyContent="space-evenly" alignItems="center" item xs={12} container>
      <Grid item xs={12} sm={10}>
        <OutletInfo outlet={props.outlet.outlet} />
      </Grid>
      <Typography>{props.outlet.count}</Typography>
    </Grid>
  </Grid>
);

export default OutletCount;
