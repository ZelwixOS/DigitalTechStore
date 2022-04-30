import React from 'react';
import { makeStyles, Theme, createStyles } from '@material-ui/core/styles';
import Grid from '@material-ui/core/Grid';
import Accordion from '@material-ui/core/Accordion';
import AccordionSummary from '@material-ui/core/AccordionSummary';
import Typography from '@material-ui/core/Typography';
import AccordionDetails from '@material-ui/core/AccordionDetails';
import ExpandMoreIcon from '@material-ui/icons/ExpandMore';
import List from '@material-ui/core/List';
import ListItem from '@material-ui/core/ListItem';
import Divider from '@material-ui/core/Divider';
import { Observer } from 'mobx-react';

import ParameterBlock from 'src/Types/ParameterBlock';
import FilterValue from 'src/Types/FilterValue';

import RangeFilter from './RangeFilter';
import ListFilter from './ListFilter';

const useStyles = makeStyles((theme: Theme) =>
  createStyles({
    heading: {
      marginLeft: theme.spacing(1),
      fontSize: 1.1 * theme.typography.fontSize,
    },
  }),
);

interface IFilterBlock {
  parameterBlock: ParameterBlock;
  pickedParams: FilterValue[];
  setParameter: (newValue: FilterValue) => void;
}

const FilterBlock: React.FC<IFilterBlock> = props => {
  const classes = useStyles();
  return (
    <Grid item xs={12}>
      <Accordion variant="outlined">
        <AccordionSummary expandIcon={<ExpandMoreIcon />} aria-controls="panel1a-content" id="panel1a-header">
          <Typography className={classes.heading}>{props.parameterBlock.name}</Typography>
        </AccordionSummary>
        <AccordionDetails>
          <Grid item xs={12}>
            <List component="nav" aria-label="main mailbox folders">
              {props.parameterBlock.parameters.map((val, index) => (
                <React.Fragment key={index}>
                  <Divider />
                  <ListItem>
                    {val.range ? (
                      <Observer>
                        {() => (
                          <RangeFilter
                            pickedParams={props.pickedParams}
                            setParameter={props.setParameter}
                            parameter={val}
                          />
                        )}
                      </Observer>
                    ) : (
                      <Observer>
                        {() => (
                          <ListFilter
                            pickedParams={props.pickedParams}
                            setParameter={props.setParameter}
                            parameter={val}
                          />
                        )}
                      </Observer>
                    )}
                  </ListItem>
                </React.Fragment>
              ))}
            </List>
          </Grid>
        </AccordionDetails>
      </Accordion>
    </Grid>
  );
};

export default FilterBlock;
