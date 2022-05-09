import React, { useEffect, useState } from 'react';
import Grid from '@material-ui/core/Grid';
import { makeStyles, createStyles, Theme } from '@material-ui/core/styles';
import Typography from '@material-ui/core/Typography';

import NavigationBar from 'src/Components/Parts/NavigationBar';
import PurchaseDetailedInfo from 'src/Components/Parts/PurchaseDetailedInfo';
import { getPurchases } from 'src/Requests/GetRequests';
import Purchase from 'src/Types/Purchase';

const useStyles = makeStyles((theme: Theme) =>
  createStyles({
    productGrid: {
      paddingLeft: theme.spacing(1),
      paddingTop: theme.spacing(1),
      paddingBottom: theme.spacing(2),
    },
    filterPanel: {
      paddingTop: theme.spacing(1),
    },
    pageName: {
      paddingTop: theme.spacing(2),
      fontWeight: theme.typography.fontWeightBold,
    },
  }),
);

const UserPurchases: React.FC = () => {
  const getProd = async (isMounted: boolean) => {
    const res = await getPurchases();
    if (isMounted) {
      setPurchases(res);
    }
  };

  const [purchases, setPurchases] = useState<Purchase[]>([]);

  const dateParse = (date: string) => {
    const val = new Date(date);
    return val.toLocaleString();
  };

  useEffect(() => {
    let isMounted = true;
    getProd(isMounted);
    return () => {
      isMounted = false;
    };
  }, []);

  const classes = useStyles();

  return (
    <React.Fragment>
      <NavigationBar />
      <Grid container direction="row" justify="center" alignItems="center">
        <Grid xs={12} sm={9} item direction="row" justify="center" alignItems="center" container>
          <Grid item direction="column" justify="center" container>
            <Grid>
              <Typography align="center" className={classes.pageName} variant="h5" component="h5">
                История заказов
              </Typography>
            </Grid>
            <Grid item direction="row" justify="center" container>
              <Grid className={classes.productGrid} xs={12} sm={9} item container direction="column">
                <Grid>
                  {purchases.map(purchase => (
                    <PurchaseDetailedInfo
                      key={purchase.id}
                      purchase={purchase}
                      cardName={`Заказ от ${dateParse(purchase.createdDate)}`}
                    />
                  ))}
                </Grid>
              </Grid>
            </Grid>
          </Grid>
        </Grid>
      </Grid>
    </React.Fragment>
  );
};

export default UserPurchases;
