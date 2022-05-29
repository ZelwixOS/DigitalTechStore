import React from 'react';
import { Typography, Grid, Card, makeStyles, Theme, createStyles, Link } from '@material-ui/core';

import NavigationBar from 'src/Components/Parts/NavigationBar';
import { getCityOulets } from 'src/Requests/GetRequests';
import Outlet from 'src/Types/Outlet';
import OutletInfo from 'src/Components/Parts/OutletInfo';

const useStyles = makeStyles((theme: Theme) =>
  createStyles({
    card: {
      margin: theme.spacing(1),
    },
    outletGrid: {
      padding: theme.spacing(1),
    },
  }),
);

const Shops = () => {
  const classes = useStyles();

  const getData = async (isMounted: boolean) => {
    const res = await getCityOulets();
    if (isMounted) {
      setOutlets(res);
    }
  };

  const refreshData = () => {
    let isMounted = true;
    getData(isMounted);

    return () => {
      isMounted = false;
    };
  };

  React.useEffect(() => {
    refreshData();
  }, []);

  const [outlets, setOutlets] = React.useState<Outlet[]>([]);

  return (
    <React.Fragment>
      <NavigationBar />
      <Grid container justifyContent="center" alignItems="center">
        <Grid xs={12} sm={9} item direction="column" justifyContent="center" alignItems="center" container>
          <Typography variant="h4" style={{ paddingBottom: '10px' }}>
            Магазины в вашем городе
          </Typography>
          <Grid container direction="column">
            {outlets.map(o => (
              <Card elevation={3} className={classes.card} key={o.id}>
                <Grid className={classes.outletGrid} container direction="row" justifyContent="center">
                  <Grid container justifyContent="flex-start" alignContent="center" item xs={12} sm={6}>
                    <OutletInfo outlet={o} />
                  </Grid>
                  <Grid container justifyContent="center" alignContent="center" item xs={12} sm={5}>
                    <Link href={`tel:${o.phoneNumber}}`}>{o.phoneNumber}</Link>
                  </Grid>
                </Grid>
              </Card>
            ))}
          </Grid>
        </Grid>
      </Grid>
    </React.Fragment>
  );
};

export default Shops;
