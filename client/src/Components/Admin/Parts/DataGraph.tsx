import React from 'react';
import { useTheme } from '@material-ui/core/styles';
import { Typography } from '@material-ui/core';
import { LineChart, Line, XAxis, YAxis, Label } from 'recharts';

interface IDataGraph {
  name: string;
  data: [];
  xDataName: string;
  xName: string;
  yDataName: string;
  yName: string;
}

export const DataGraph: React.FC<IDataGraph> = props => {
  const theme = useTheme();

  return (
    <React.Fragment>
      <Typography component="h2" variant="h6" color="primary" gutterBottom>
        {props.name}
      </Typography>
      <LineChart data={props.data}>
        <XAxis dataKey={props.xDataName} stroke={theme.palette.text.primary}>
          <Label>{props.xName}</Label>
        </XAxis>
        <YAxis stroke={theme.palette.text.primary}>
          <Label>{props.yName}</Label>
        </YAxis>
        <Line type="monotone" dataKey={props.yDataName} stroke={theme.palette.primary.main} />
      </LineChart>
    </React.Fragment>
  );
};
