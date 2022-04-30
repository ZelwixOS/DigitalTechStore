import React from 'react';
import Divider from '@material-ui/core/Divider';
import ListItem from '@material-ui/core/ListItem';
import Typography from '@material-ui/core/Typography';
import Button from '@material-ui/core/Button';
import Card from '@material-ui/core/Card';
import Grid from '@material-ui/core/Grid';
import List from '@material-ui/core/List';
import TextField from '@material-ui/core/TextField';
import InputAdornment from '@material-ui/core/InputAdornment';
import { createStyles, makeStyles, Theme } from '@material-ui/core/styles';
import { Observer } from 'mobx-react';

import ParameterBlock from 'src/Types/ParameterBlock';
import FilterValue from 'src/Types/FilterValue';

import FilterBlock from './FilterBlock';

const useStyles = makeStyles((theme: Theme) =>
  createStyles({
    rightTextField: {
      marginLeft: theme.spacing(1),
    },
    leftTextField: {
      marginRight: theme.spacing(1),
    },
  }),
);

interface IFilterBar {
  priceRange: number[];
  pickedPrices: number[];
  parameterBlocks: ParameterBlock[];
  pickedParams: FilterValue[];
  applyChanges: () => void;
  setPrices: (i: number, newValue: unknown) => void;
  setParameter: (newValue: FilterValue) => void;
}

const FilterBar: React.FC<IFilterBar> = props => {
  const handleMinChange = (event: React.ChangeEvent<{ value: unknown }>) => {
    props.setPrices(0, event.target.value);
  };

  const handleMaxChange = (event: React.ChangeEvent<{ value: unknown }>) => {
    props.setPrices(1, event.target.value);
  };

  const classes = useStyles();

  return (
    <Grid>
      <Card variant="outlined">
        <List component="nav" aria-label="main mailbox folders">
          <ListItem>
            <Typography variant="h5" component="h5">
              Фильтры
            </Typography>
          </ListItem>
          <Divider variant="middle" />
          <ListItem>
            <Typography id="price-slider" gutterBottom>
              Цена
            </Typography>
          </ListItem>
          <ListItem>
            <Observer>
              {() => (
                <TextField
                  id="minPrice"
                  variant="outlined"
                  placeholder={props.priceRange[0].toString()}
                  size="small"
                  type="number"
                  className={classes.leftTextField}
                  value={props.pickedPrices[0]}
                  onChange={handleMinChange}
                  InputProps={{
                    endAdornment: <InputAdornment position="end">₽</InputAdornment>,
                  }}
                />
              )}
            </Observer>
            <Observer>
              {() => (
                <TextField
                  id="maxPrice"
                  variant="outlined"
                  placeholder={props.priceRange[1].toString()}
                  size="small"
                  type="number"
                  className={classes.rightTextField}
                  value={props.pickedPrices[1]}
                  onChange={handleMaxChange}
                  InputProps={{
                    endAdornment: <InputAdornment position="end">₽</InputAdornment>,
                  }}
                />
              )}
            </Observer>
          </ListItem>
          <Observer>
            {() => (
              <React.Fragment>
                {props.parameterBlocks &&
                  props.parameterBlocks.map((val, index) => (
                    <React.Fragment key={index}>
                      <ListItem>
                        <FilterBlock
                          parameterBlock={val}
                          setParameter={props.setParameter}
                          pickedParams={props.pickedParams}
                        />
                      </ListItem>
                    </React.Fragment>
                  ))}
              </React.Fragment>
            )}
          </Observer>
          <Divider variant="middle" />
          <ListItem>
            <Grid container justify="center">
              <Button variant="contained" color="primary" onClick={props.applyChanges}>
                Применить
              </Button>
            </Grid>
          </ListItem>
        </List>
      </Card>
    </Grid>
  );
};
export default FilterBar;
