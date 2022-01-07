import React, { useState } from 'react';
import { makeStyles, Theme, createStyles } from '@material-ui/core/styles';
import InputLabel from '@material-ui/core/InputLabel';
import MenuItem from '@material-ui/core/MenuItem';
import FormControl from '@material-ui/core/FormControl';
import Select from '@material-ui/core/Select';
import Card from '@material-ui/core/Card';
import Grid from '@material-ui/core/Grid';
import ArrowDownwardIcon from '@material-ui/icons/ArrowDownward';
import ArrowUpwardIcon from '@material-ui/icons/ArrowUpward';
import { Observer } from 'mobx-react';

const useStyles = makeStyles((theme: Theme) =>
  createStyles({
    selector: {
      marginLeft: theme.spacing(6),
    },
    formControl: {
      margin: theme.spacing(1),
      minWidth: 100,
    },
  }),
);

interface ISortBar {
  type: string;
  onChange: (sortType: string) => void;
}

const SortBar: React.FC<ISortBar> = props => {
  const classes = useStyles();
  const [open, setOpen] = useState(false);

  const handleOpen = () => {
    setOpen(!open);
  };

  const handleSortTypeChanged = (event: React.ChangeEvent<{ value: unknown }>) => {
    props.onChange(event.target.value as string);
  };

  return (
    <Grid item xs={12}>
      <Card variant="outlined">
        <Grid className={classes.selector} container alignItems="center" justify="flex-start">
          <FormControl className={classes.formControl}>
            <InputLabel id="sorting-controlled-open-select-label">Сортировка</InputLabel>
            <Observer>
              {() => (
                <Select
                  labelId="sorting-controlled-open-select-label"
                  id="sorting-controlled-open-select"
                  open={open}
                  onClick={handleOpen}
                  value={props.type}
                  onChange={handleSortTypeChanged}
                >
                  <MenuItem value={'price'}>
                    <ArrowDownwardIcon fontSize="small" /> Цена
                  </MenuItem>
                  <MenuItem value={'price reverse'}>
                    <ArrowUpwardIcon fontSize="small" /> Цена
                  </MenuItem>
                  <MenuItem value={'name'}>
                    <ArrowDownwardIcon fontSize="small" /> Название
                  </MenuItem>
                  <MenuItem value={'name reverse'}>
                    <ArrowUpwardIcon fontSize="small" /> Название
                  </MenuItem>
                </Select>
              )}
            </Observer>
          </FormControl>
        </Grid>
      </Card>
    </Grid>
  );
};

export default SortBar;
