import React from 'react';
import Grid from '@material-ui/core/Grid';
import Typography from '@material-ui/core/Typography';

import Outlet from 'src/Types/Outlet';

interface IOutletInfo {
  outlet: Outlet;
}

const OutletInfo: React.FC<IOutletInfo> = props => (
  <Grid item xs={12} direction="column" justify="space-evenly" alignItems="stretch" container>
    <Typography variant="h6" color="primary">
      {props.outlet.name}
    </Typography>
    <Typography>
      {`${props.outlet.region.name}, ${props.outlet.city.name}, ${props.outlet.streetName}, ${props.outlet.building}`}
    </Typography>
    {props.outlet.userNote && <Typography color="textSecondary">{props.outlet.userNote}</Typography>}
  </Grid>
);

export default OutletInfo;
