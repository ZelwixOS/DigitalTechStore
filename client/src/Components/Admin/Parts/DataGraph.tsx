import React from 'react';
import { useTheme } from '@material-ui/core/styles';
import { createStyles, makeStyles, Paper, Theme, Typography } from '@material-ui/core';
import { LineChart, Line, XAxis, YAxis, Tooltip } from 'recharts';

import SalesTimeStatistics from 'src/Types/SalesTimeStatistics';

const useStyles = makeStyles((theme: Theme) =>
  createStyles({
    title: {
      margin: theme.spacing(2, 4),
      textShadow: '#CCC 1px 1px 2px',
    },
    chart: {
      margin: theme.spacing(1, 2, 1, 1),
    },
  }),
);

interface IDataGraph {
  name: string;
  data: SalesTimeStatistics[];
  xName: string;
  yName: string;
  color: string;
}

export const DataGraph: React.FC<IDataGraph> = props => {
  const theme = useTheme();
  const classes = useStyles();

  return (
    <React.Fragment>
      <Typography align="center" variant="h5" style={{ color: props.color }} className={classes.title} gutterBottom>
        {props.name}
      </Typography>
      <Paper variant="outlined">
        <LineChart width={500} height={200} data={props.data} className={classes.chart}>
          <Line type="monotone" dataKey="data" stroke={props.color} name={props.yName} />
          <XAxis dataKey="date" name={props.xName} />
          <Tooltip />
        </LineChart>
      </Paper>
    </React.Fragment>
  );
};
