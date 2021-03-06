import React from 'react';
import { createStyles, Divider, Grid, List, makeStyles, Paper, Theme, Typography } from '@material-ui/core';

import NavigationBar from 'src/Components/Admin/Parts/NavigationBar';
import { getMonthSales, getOrdersForMonth, getTotalSales, getWorkersSales } from 'src/Requests/GetRequests';
import { DataGraph } from 'src/Components/Admin/Parts/DataGraph';
import SalesTimeStatistics from 'src/Types/SalesTimeStatistics';
import { DonutSalesStatisticsDiagram } from 'src/Components/Admin/Parts/DonutSalesStatisticsDiagram';
import SalesStatistics from 'src/Types/SalesStatistics';
import CreateFileReport from 'src/Components/Admin/Parts/CreateFileReport';
import { DonutSellers } from 'src/Components/Admin/Parts/DonutSellers';
import WorkersSales from 'src/Types/WorkersSales';

const useStyles = makeStyles((theme: Theme) =>
  createStyles({
    mainTitle: {
      fontWeight: theme.typography.fontWeightBold,
    },
    title: {
      paddingTop: theme.spacing(2),
      paddingBottom: theme.spacing(2),
    },
    mainGrid: {
      paddingBottom: theme.spacing(2),
    },
  }),
);

export const AdminPage = () => {
  const classes = useStyles();

  const getData = async (isMounted: boolean) => {
    const resPFM = await getOrdersForMonth(0, true);
    const resPrCFM = await getOrdersForMonth(1, true);
    const resMAFM = await getOrdersForMonth(2, true);

    const resPFMA = await getMonthSales(0);
    const resPrCFMA = await getMonthSales(1);
    const resMAFMA = await getMonthSales(2);

    const resPTA = await getTotalSales(0);
    const resPrCTA = await getTotalSales(1);
    const resMATA = await getTotalSales(2);

    const resPFMW = await getWorkersSales(0);
    const resPrCFMW = await getWorkersSales(1);
    const resMAFMW = await getWorkersSales(2);

    if (isMounted) {
      setPurchasesForMonth(arrDateParser(resPFM));
      setProductsForMonth(arrDateParser(resPrCFM));
      setMoneyForMonth(arrDateParser(resMAFM));

      setPurchasesForMonthAll(resPFMA);
      setProductsForMonthAll(resPrCFMA);
      setMoneyForMonthAll(resMAFMA);

      setPurchasesTotalAll(resPTA);
      setProductsTotalAll(resPrCTA);
      setMoneyTotalAll(resMATA);

      setPurchasesWorkers(resPFMW);
      setProductsWorkers(resPrCFMW);
      setMoneyWorkers(resMAFMW);
    }
  };

  const arrDateParser = (orig: SalesTimeStatistics[]) => {
    const arr = [] as SalesTimeStatistics[];
    orig.forEach(function (item) {
      const newItem = { data: item.data, date: dateParse(item.date) };
      arr.push(newItem);
    });

    return arr;
  };

  const dateParse = (date: string) => {
    const val = new Date(date);
    return val.toLocaleDateString().substring(0, 5);
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

  const getDefaultSalesStat = () => ({ finished: 1, notCompleted: 1, refused: 1, canceled: 1 });

  const [purchasesForMonth, setPurchasesForMonth] = React.useState<SalesTimeStatistics[]>([]);
  const [productsForMonth, setProductsForMonth] = React.useState<SalesTimeStatistics[]>([]);
  const [moneyForMonth, setMoneyForMonth] = React.useState<SalesTimeStatistics[]>([]);

  const [purchasesForMonthAll, setPurchasesForMonthAll] = React.useState<SalesStatistics>(getDefaultSalesStat());
  const [productsForMonthAll, setProductsForMonthAll] = React.useState<SalesStatistics>(getDefaultSalesStat());
  const [moneyForMonthAll, setMoneyForMonthAll] = React.useState<SalesStatistics>(getDefaultSalesStat());

  const [purchasesTotalAll, setPurchasesTotalAll] = React.useState<SalesStatistics>(getDefaultSalesStat());
  const [productsTotalAll, setProductsTotalAll] = React.useState<SalesStatistics>(getDefaultSalesStat());
  const [moneyTotalAll, setMoneyTotalAll] = React.useState<SalesStatistics>(getDefaultSalesStat());

  const [purchasesWorkers, setPurchasesWorkers] = React.useState<WorkersSales>({ names: [], data: [] });
  const [productsWorkers, setProductsWorkers] = React.useState<WorkersSales>({ names: [], data: [] });
  const [moneyWorkers, setMoneyWorkers] = React.useState<WorkersSales>({ names: [], data: [] });

  const role = sessionStorage.getItem('signed');

  return (
    <React.Fragment>
      <NavigationBar />
      <List>
        <Typography variant="h3" align="center" className={classes.mainTitle}>
          ?????????? ????????????????????
        </Typography>
        <Divider />
        <Typography variant="h4" align="center" className={classes.title}>
          ?????????????????????? ????????????
        </Typography>
        <Grid container direction="row" justifyContent="space-evenly" className={classes.mainGrid}>
          <Grid item xs={12} sm={4} container justifyContent="center">
            <Paper elevation={3}>
              <DataGraph
                color="#7C07A9"
                data={purchasesForMonth}
                xName="????????"
                yName="?????????? ??????????????"
                name="?????????????? ???? ??????????"
              />
            </Paper>
          </Grid>
          <Grid item xs={12} sm={4} container justifyContent="center">
            <Paper elevation={3}>
              <DataGraph
                color="#FFA100"
                data={productsForMonth}
                xName="????????"
                yName="???????????????????? ?????????????????? ??????????????????"
                name="???????????? ???? ??????????"
              />
            </Paper>
          </Grid>
          <Grid item xs={12} sm={4} container justifyContent="center">
            <Paper elevation={3}>
              <DataGraph color="#0ACF00" data={moneyForMonth} xName="????????" yName="??????????????" name="?????????????? ???? ??????????" />
            </Paper>
          </Grid>
        </Grid>
        <Divider />
        <Typography variant="h4" align="center" className={classes.title}>
          ???????????? ???? ??????????
        </Typography>
        <Grid container direction="row" justifyContent="space-evenly" className={classes.mainGrid}>
          <Grid item xs={12} sm={4} container justifyContent="center">
            <Paper elevation={3}>
              <DonutSalesStatisticsDiagram
                colors={['#906CD7', '#7746D7', '#2A0671', '#482A83']}
                data={purchasesForMonthAll}
                name="???????????????????? ?????????????? ???? ??????????"
              />
            </Paper>
          </Grid>
          <Grid item xs={12} sm={4} container justifyContent="center">
            <Paper elevation={3}>
              <DonutSalesStatisticsDiagram
                colors={['#FFBE73', '#FFA640', '#A65900', '#FF8900']}
                data={productsForMonthAll}
                name="???????????????????? ?????????????????? ???? ??????????"
              />
            </Paper>
          </Grid>
          <Grid item xs={12} sm={4} container justifyContent="center">
            <Paper elevation={3}>
              <DonutSalesStatisticsDiagram
                colors={['#B0F26D', '#98F23D', '#4B9500', '#6CAC2B']}
                data={moneyForMonthAll}
                name="???????????????????? ?????????????? ???? ??????????"
              />
            </Paper>
          </Grid>
        </Grid>
        <Divider />
        <Typography variant="h4" align="center" className={classes.title}>
          ???????????? ???? ?????? ??????????
        </Typography>
        <Grid container direction="row" justifyContent="space-evenly" className={classes.mainGrid}>
          <Grid item xs={12} sm={4} container justifyContent="center">
            <Paper elevation={3}>
              <DonutSalesStatisticsDiagram
                colors={['#906CD7', '#7746D7', '#2A0671', '#482A83']}
                data={purchasesTotalAll}
                name="???????????????????? ??????????????"
              />
            </Paper>
          </Grid>
          <Grid item xs={12} sm={4} container justifyContent="center">
            <Paper elevation={3}>
              <DonutSalesStatisticsDiagram
                colors={['#FFBE73', '#FFA640', '#A65900', '#FF8900']}
                data={productsTotalAll}
                name="???????????????????? ??????????????????"
              />
            </Paper>
          </Grid>
          <Grid item xs={12} sm={4} container justifyContent="center">
            <Paper elevation={3}>
              <DonutSalesStatisticsDiagram
                colors={['#B0F26D', '#98F23D', '#4B9500', '#6CAC2B']}
                data={moneyTotalAll}
                name="???????????????????? ??????????????"
              />
            </Paper>
          </Grid>
        </Grid>
        <Divider />
        {role !== 'Admin' && purchasesWorkers?.data?.length > 0 && (
          <React.Fragment>
            <Typography variant="h4" align="center" className={classes.title}>
              ?????????????? ????????????????????
            </Typography>
            <Grid container direction="row" justifyContent="space-evenly" className={classes.mainGrid}>
              <Grid item xs={12} sm={4} container justifyContent="center">
                <Paper elevation={3}>
                  <DonutSellers
                    colors={['#906CD7', '#7746D7', '#2A0671', '#482A83']}
                    data={purchasesWorkers}
                    name="???????????????????? ??????????????"
                  />
                </Paper>
              </Grid>
              <Grid item xs={12} sm={4} container justifyContent="center">
                <Paper elevation={3}>
                  <DonutSellers
                    colors={['#FFBE73', '#FFA640', '#A65900', '#FF8900']}
                    data={productsWorkers}
                    name="???????????????????? ??????????????????"
                  />
                </Paper>
              </Grid>
              <Grid item xs={12} sm={4} container justifyContent="center">
                <Paper elevation={3}>
                  <DonutSellers
                    colors={['#B0F26D', '#98F23D', '#4B9500', '#6CAC2B']}
                    data={moneyWorkers}
                    name="???????????????????? ??????????????"
                  />
                </Paper>
              </Grid>
            </Grid>
            <Divider />
          </React.Fragment>
        )}
        <Typography variant="h4" align="center" className={classes.title}>
          ???????????????????????? ?????????? ???? ????????????
        </Typography>
        <CreateFileReport />
      </List>
    </React.Fragment>
  );
};
