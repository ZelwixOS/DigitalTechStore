import React, { useState } from 'react';
import Typography from '@material-ui/core/Typography';
import { createStyles, makeStyles, Theme } from '@material-ui/core/styles';
import Grid from '@material-ui/core/Grid';
import Rating from '@material-ui/lab/Rating';
import Card from '@material-ui/core/Card';
import { Button } from '@material-ui/core';

import Review from 'src/Types/Review';
import { banReview } from 'src/Requests/PostRequests';

interface IProductReviews {
  review: Review;
}

const useStyles = makeStyles((theme: Theme) =>
  createStyles({
    userName: {
      fontSize: 1.1 * theme.typography.fontSize,
      fontWeight: theme.typography.fontWeightBold,
    },
    reviewHeader: {
      padding: theme.spacing(1),
      borderBottom: `1px solid ${theme.palette.divider}`,
    },
    commentSection: {
      minHeight: '6em',
      padding: theme.spacing(2),
    },
    root: {
      margin: theme.spacing(2, 1, 0, 1),
    },
  }),
);

const ReviewBlock: React.FC<IProductReviews> = props => {
  const classes = useStyles();

  const role = sessionStorage.getItem('signed');
  const showBan = role && (role === 'Admin' || role === 'Manager');

  const [show, setShow] = useState(true);

  const ban = async () => {
    const res = await banReview(props.review.id);
    if (res > 0) {
      setShow(false);
    }
  };

  return (
    <React.Fragment>
      {show && (
        <Card variant="outlined" className={classes.root}>
          <Grid item xs={12} container direction="column" alignItems="flex-start" justifyContent="flex-start">
            <Grid
              className={classes.reviewHeader}
              container
              direction="row"
              alignItems="center"
              justifyContent="flex-start"
            >
              <Grid item xs={12} sm={showBan ? 4 : 8}>
                <Typography align="center" className={classes.userName}>
                  {props.review.userName}
                </Typography>
              </Grid>
              <Grid item xs={12} sm={4}>
                <Rating value={props.review.mark} readOnly />
              </Grid>
              <Grid item xs={12} sm={4} container justifyContent="flex-end">
                {showBan && (
                  <Button variant="outlined" onClick={ban}>
                    ??????????????????????????
                  </Button>
                )}
              </Grid>
            </Grid>
            <Typography className={classes.commentSection}>{props.review.description}</Typography>
          </Grid>
        </Card>
      )}
    </React.Fragment>
  );
};

export default ReviewBlock;
