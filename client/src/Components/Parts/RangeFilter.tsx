import React, { useState } from 'react';
import { Observer } from 'mobx-react';
import { makeStyles, Theme, createStyles } from '@material-ui/core/styles';
import Grid from '@material-ui/core/Grid';
import TextField from '@material-ui/core/TextField';
import Typography from '@material-ui/core/Typography';

import Parameter from 'src/Types/Parameter';
import FilterValue from 'src/Types/FilterValue';

const useStyles = makeStyles((theme: Theme) =>
  createStyles({
    heading: {
      textDecoration: 'underline',
    },
    beforeText: {
      paddingRight: theme.spacing(1),
    },
  }),
);

interface IRangeFilter {
  parameter: Parameter;
  pickedParams: FilterValue[];
  setParameter: (newValue: FilterValue) => void;
}

const RangeFilter: React.FC<IRangeFilter> = props => {
  const classes = useStyles();
  const [minValue, setMinValue] = useState<string>('');
  const [maxValue, setMaxValue] = useState<string>('');
  const paramValue = props.pickedParams.find(i => i.id === props.parameter.id);

  const handleMinChange = (event: React.ChangeEvent<{ value: unknown }>) => {
    if (paramValue) {
      paramValue.minValue = event.target.value as string;
    }

    setMinValue(event.target.value as string);
    props.setParameter({
      id: props.parameter.id,
      minValue: event.target.value as string,
      maxValue: paramValue?.maxValue,
      range: true,
    });
  };

  const handleMaxChange = (event: React.ChangeEvent<{ value: unknown }>) => {
    if (paramValue) {
      paramValue.maxValue = event.target.value as string;
    }

    setMaxValue(event.target.value as string);
    props.setParameter({
      id: props.parameter.id,
      minValue: paramValue?.minValue ?? minValue,
      maxValue: event.target.value as string,
      range: true,
    });
  };

  return (
    <Grid item container justify="space-evenly" alignItems="flex-start" direction="column" xs={12}>
      <Typography className={classes.heading}>{props.parameter.name}:</Typography>
      <Grid item container justify="flex-start" alignItems="center" direction="row" xs={12}>
        <Typography className={classes.beforeText} variant="body1">
          От:
        </Typography>
        <Observer>
          {() => (
            <TextField
              id="minPrice"
              variant="outlined"
              placeholder={props.parameter.minValue.toString()}
              size="small"
              type="number"
              value={paramValue?.minValue ?? minValue}
              onChange={handleMinChange}
            />
          )}
        </Observer>
      </Grid>
      <Grid item container justify="flex-start" alignItems="center" direction="row" xs={12}>
        <Typography className={classes.beforeText} variant="body1">
          До:
        </Typography>
        <Observer>
          {() => (
            <TextField
              id="minPrice"
              variant="outlined"
              placeholder={props.parameter.maxValue.toString()}
              size="small"
              type="number"
              value={paramValue?.maxValue ?? maxValue}
              onChange={handleMaxChange}
            />
          )}
        </Observer>
      </Grid>
    </Grid>
  );
};

export default RangeFilter;
