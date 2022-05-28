import Button from '@material-ui/core/Button';
import Grid from '@material-ui/core/Grid';
import TextField from '@material-ui/core/TextField';
import React, { useState } from 'react';
import { createStyles, makeStyles, Theme } from '@material-ui/core/styles';

const useStyles = makeStyles((theme: Theme) =>
  createStyles({
    root: {
      padding: theme.spacing(1),
    },
  }),
);

interface IPoductCounter {
  count: number;
  id?: string;
  onCount?: (newCount: number, id?: string) => void;
}

const PoductCounter: React.FC<IPoductCounter> = props => {
  const [currentCount, setCount] = useState<number>(props.count);
  const classes = useStyles();

  const onCount = (newCount: number) => {
    let countValue = newCount;
    if (newCount > 99) {
      countValue = 99;
    } else if (newCount < 1) {
      countValue = 1;
    }

    if (currentCount !== countValue) {
      if (props.onCount) {
        props.onCount(countValue, props.id);
      }

      setCount(countValue);
    }
  };

  const addCount = () => {
    const curCount = currentCount + 1;
    onCount(curCount);
  };

  const minCount = () => {
    const curCount = currentCount - 1;
    onCount(curCount);
  };

  const onlyNubmers = /^\d[0-2]$/;
  const onTextChanged = (event: React.ChangeEvent<{ value: unknown }>) => {
    const curStringCount = event.target.value as string;
    if (onlyNubmers.test(curStringCount)) {
      onCount(parseInt(curStringCount));
    } else {
      onCount(1);
    }
  };

  return (
    <Grid className={classes.root} container direction="column" justifyContent="center" alignItems="center">
      <Button variant="outlined" onClick={addCount}>
        +
      </Button>
      <TextField id="counter" value={currentCount} onChange={onTextChanged} variant="outlined" />
      <Button variant="outlined" onClick={minCount}>
        -
      </Button>
    </Grid>
  );
};

export default PoductCounter;
