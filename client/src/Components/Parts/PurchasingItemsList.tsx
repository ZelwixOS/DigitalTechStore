import React from 'react';
import Grid from '@material-ui/core/Grid';
import Typography from '@material-ui/core/Typography';
import { Button, Divider, List, ListItem } from '@material-ui/core';
import { createStyles, makeStyles, Theme } from '@material-ui/core/styles';

import PurchaseItem from 'src/Types/PurchaseItem';

const useStyles = makeStyles((theme: Theme) =>
  createStyles({
    text: {
      marginTop: theme.spacing(1),
      marginBottom: theme.spacing(1),
    },
  }),
);

interface IPurchasingItemsList {
  purchaseItems: PurchaseItem[];
}

const PurchasingItemsList: React.FC<IPurchasingItemsList> = props => {
  const classes = useStyles();
  return (
    <List>
      <Divider />
      {props.purchaseItems.map(item => (
        <React.Fragment key={item.productId}>
          <ListItem>
            <Grid container direction="row" justifyContent="space-around">
              <Button className={classes.text} href={`product/${item.productId}`}>{`${item.productName}`}</Button>
              <Typography className={classes.text}>{`${item.price}₽ x${item.count}`}</Typography>
            </Grid>
          </ListItem>
          <Divider />
        </React.Fragment>
      ))}
    </List>
  );
};
export default PurchasingItemsList;
