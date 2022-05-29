import React from 'react';
import { Chart as ChartJS, ArcElement, Tooltip, Legend } from 'chart.js';
import { useTheme } from '@material-ui/core/styles';
import { createStyles, Grid, makeStyles, Paper, Theme, Typography } from '@material-ui/core';
import { Doughnut } from 'react-chartjs-2';

import SalesStatistics from 'src/Types/SalesStatistics';

ChartJS.register(ArcElement, Tooltip, Legend);

const useStyles = makeStyles((theme: Theme) =>
  createStyles({
    title: {
      margin: theme.spacing(2, 4),
      textShadow: '#CCC 1px 1px 2px',
    },
    chart: {
      margin: theme.spacing(1, 2, 1, 1),
      minWidth: 500,
      minHeight: 500,
    },
    doughnut: {
      padding: theme.spacing(1),
    },
  }),
);

interface IDonutSalesStatisticsDiagram {
  name: string;
  data: SalesStatistics;
  colors: string[];
}

export const DonutSalesStatisticsDiagram: React.FC<IDonutSalesStatisticsDiagram> = props => {
  const theme = useTheme();
  const classes = useStyles();

  const pieChartData = {
    labels: ['Завершены', 'Выполняются', 'Отказ', 'Отменены'],
    datasets: [
      {
        label: 'Число покупок',
        data: [props.data.finished, props.data.notCompleted, props.data.refused, props.data.canceled],
        backgroundColor: props.colors,
        hoverOffset: 10,
      },
    ],
  };

  return (
    <Grid
      item
      xs={12}
      direction="column"
      container
      justifyContent="flex-start"
      alignContent="center"
      className={classes.chart}
    >
      <Typography align="center" variant="h5" className={classes.title} gutterBottom>
        {props.name}
      </Typography>
      <Paper variant="outlined">
        <Doughnut className={classes.doughnut} data={pieChartData} />
      </Paper>
    </Grid>
  );
};
