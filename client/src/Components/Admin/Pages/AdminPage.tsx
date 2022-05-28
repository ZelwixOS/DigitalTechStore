import React from 'react';
import { Grid, Paper } from '@material-ui/core';

import NavigationBar from 'src/Components/Admin/Parts/NavigationBar';
import { getOrdersForMonth } from 'src/Requests/GetRequests';
import { DataGraph } from 'src/Components/Admin/Parts/DataGraph';

export const AdminPage = () => {
  const getData = async (isMounted: boolean) => {
    const resPFM = await getOrdersForMonth(0, false);
    if (isMounted) {
      setPurchasesForMonth(resPFM as []);
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

  const [purchasesForMonth, setPurchasesForMonth] = React.useState([] as []);

  return (
    <React.Fragment>
      <NavigationBar />
      <Grid container spacing={3}>
        <Grid item xs={12} md={8} lg={9}>
          <Paper>
            {purchasesForMonth.length > 0 && (
              <DataGraph
                data={purchasesForMonth}
                xName="Дата"
                yName="Число покупок"
                name="Покупки за месяц"
                xDataName="date"
                yDataName="data"
              />
            )}
          </Paper>
        </Grid>
      </Grid>
    </React.Fragment>
  );
};
