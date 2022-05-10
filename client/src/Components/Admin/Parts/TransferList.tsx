import React from 'react';
import { makeStyles, Theme, createStyles } from '@material-ui/core/styles';
import Grid from '@material-ui/core/Grid';
import List from '@material-ui/core/List';
import ListItem from '@material-ui/core/ListItem';
import ListItemIcon from '@material-ui/core/ListItemIcon';
import ListItemText from '@material-ui/core/ListItemText';
import Checkbox from '@material-ui/core/Checkbox';
import Button from '@material-ui/core/Button';
import Paper from '@material-ui/core/Paper';

const useStyles = makeStyles((theme: Theme) =>
  createStyles({
    root: {
      margin: 'auto',
    },
    paper: {
      width: 200,
      height: 230,
      overflow: 'auto',
    },
    button: {
      margin: theme.spacing(0.5, 0),
    },
  }),
);

function not<Type>(a: IIdentifieable<Type>[], b: IIdentifieable<Type>[]) {
  return a.filter(value => b.indexOf(value) === -1);
}

function intersection<Type>(a: IIdentifieable<Type>[], b: IIdentifieable<Type>[]) {
  return a.filter(value => b.indexOf(value) !== -1);
}

export interface IIdentifieable<Type> {
  id: Type;
  name: string;
}

interface ITransferList<Type> {
  left: IIdentifieable<Type>[];
  right: IIdentifieable<Type>[];
  setLeft: React.Dispatch<React.SetStateAction<IIdentifieable<Type>[]>>;
  setRight: React.Dispatch<React.SetStateAction<IIdentifieable<Type>[]>>;
}

export const TransferList = <Type,>(props: ITransferList<Type>) => {
  const classes = useStyles();
  const [checked, setChecked] = React.useState<IIdentifieable<Type>[]>([]);

  const leftChecked = intersection(checked, props.left);
  const rightChecked = intersection(checked, props.right);

  const handleToggle = (value: IIdentifieable<Type>) => () => {
    const currentIndex = checked.indexOf(value);
    const newChecked = [...checked];

    if (currentIndex === -1) {
      newChecked.push(value);
    } else {
      newChecked.splice(currentIndex, 1);
    }

    setChecked(newChecked);
  };

  const handleAllRight = () => {
    props.setRight(props.right.concat(props.left));
    props.setLeft([]);
  };

  const handleCheckedRight = () => {
    props.setRight(props.right.concat(leftChecked));
    props.setLeft(not(props.left, leftChecked));
    setChecked(not(checked, leftChecked));
  };

  const handleCheckedLeft = () => {
    props.setLeft(props.left.concat(rightChecked));
    props.setRight(not(props.right, rightChecked));
    setChecked(not(checked, rightChecked));
  };

  const handleAllLeft = () => {
    props.setLeft(props.left.concat(props.right));
    props.setRight([]);
  };

  const customList = (items: IIdentifieable<Type>[]) => (
    <Paper className={classes.paper}>
      <List dense component="div" role="list">
        {items.map((value: IIdentifieable<Type>) => {
          const labelId = `transfer-list-item-${value}-label`;

          return (
            <ListItem key={value.id as unknown as string} role="listitem" button onClick={handleToggle(value)}>
              <ListItemIcon>
                <Checkbox
                  checked={checked.indexOf(value) !== -1}
                  tabIndex={-1}
                  disableRipple
                  color="primary"
                  inputProps={{ 'aria-labelledby': labelId }}
                />
              </ListItemIcon>
              <ListItemText id={labelId} primary={value.name} />
            </ListItem>
          );
        })}
        <ListItem />
      </List>
    </Paper>
  );

  return (
    <Grid container spacing={2} justifyContent="center" alignItems="center" className={classes.root}>
      <Grid item>{customList(props.left)}</Grid>
      <Grid item>
        <Grid container direction="column" alignItems="center">
          <Button
            variant="outlined"
            size="small"
            className={classes.button}
            onClick={handleAllRight}
            disabled={props.left.length === 0}
            aria-label="move all right"
          >
            ≫
          </Button>
          <Button
            variant="outlined"
            size="small"
            className={classes.button}
            onClick={handleCheckedRight}
            disabled={leftChecked.length === 0}
            aria-label="move selected right"
          >
            &gt;
          </Button>
          <Button
            variant="outlined"
            size="small"
            className={classes.button}
            onClick={handleCheckedLeft}
            disabled={rightChecked.length === 0}
            aria-label="move selected left"
          >
            &lt;
          </Button>
          <Button
            variant="outlined"
            size="small"
            className={classes.button}
            onClick={handleAllLeft}
            disabled={props.right.length === 0}
            aria-label="move all left"
          >
            ≪
          </Button>
        </Grid>
      </Grid>
      <Grid item>{customList(props.right)}</Grid>
    </Grid>
  );
};
