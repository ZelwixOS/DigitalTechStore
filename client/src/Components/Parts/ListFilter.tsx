import React, { useEffect, useState } from 'react';
import { makeStyles, Theme, createStyles } from '@material-ui/core/styles';
import Grid from '@material-ui/core/Grid';
import Typography from '@material-ui/core/Typography';
import Checkbox from '@material-ui/core/Checkbox';
import FormControlLabel from '@material-ui/core/FormControlLabel';

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
    checkboxField: {
      maxHeight: 130,
      overflow: 'auto',
    },
  }),
);

interface IListFilter {
  parameter: Parameter;
  pickedParams: FilterValue[];
  setParameter: (newValue: FilterValue) => void;
}

const ListFilter: React.FC<IListFilter> = props => {
  const classes = useStyles();

  const [picked, setPicked] = useState<boolean[]>([]);
  const getChecked = (isMounted: boolean) => {
    const value = props.parameter.parameterValues.map(p =>
      props.pickedParams.find(pic => pic.id === props.parameter.id && pic.itemIds?.find(i => i === p.id))
        ? true
        : false,
    );

    if (isMounted) {
      setPicked(value);
    }
  };

  useEffect(() => {
    let isMounted = true;
    getChecked(isMounted);
    return () => {
      isMounted = false;
    };
  }, []);

  const onCheckboxChanged = (event: React.ChangeEvent, checked: boolean) => {
    const pickedValues: string[] = [];
    for (let i = 0; i < props.parameter.parameterValues.length; i++) {
      if (picked[i]) {
        pickedValues.push(props.parameter.parameterValues[i].id);
      }
    }

    const ind = props.parameter.parameterValues.findIndex(p => p.id === event.currentTarget.id);
    if (checked) {
      pickedValues.push(event.currentTarget.id);
    } else {
      const index = pickedValues.findIndex(v => v === event.currentTarget.id);
      if (index !== -1) {
        pickedValues.splice(index, 1);
      }
    }

    picked[ind] = checked;
    setPicked([...picked]);
    props.setParameter({ id: props.parameter.id, itemIds: pickedValues, range: false });
  };

  return (
    <Grid item xs={12}>
      <Typography className={classes.heading}>{props.parameter.name}:</Typography>
      <Grid item xs={12} className={classes.checkboxField}>
        {props.parameter.parameterValues.map(
          (val, index) =>
            picked[index] !== undefined && (
              <FormControlLabel
                key={index}
                control={<Checkbox color="primary" onChange={onCheckboxChanged} checked={picked[index]} id={val.id} />}
                label={val.value}
              />
            ),
        )}
      </Grid>
    </Grid>
  );
};

export default ListFilter;
