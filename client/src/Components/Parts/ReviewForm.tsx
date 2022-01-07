import React, { useState } from 'react';
import { createStyles, makeStyles, Theme } from '@material-ui/core/styles';
import Grid from '@material-ui/core/Grid';
import Rating from '@material-ui/lab/Rating';
import Card from '@material-ui/core/Card';
import TextField from '@material-ui/core/TextField';
import Button from '@material-ui/core/Button';

import Review from 'src/Types/Review';

const useStyles = makeStyles((theme: Theme) =>
  createStyles({
    root: {
      margin: theme.spacing(2, 1, 0, 1),
    },
    cardPart: {
      padding: theme.spacing(1),
    },
  }),
);

interface ICreateReview {
  mark: number;
  message: string;
}

interface IReviewForm {
  addReview: (newReview: Review) => void;
}

const ReviewForm: React.FC<IReviewForm> = props => {
  const classes = useStyles();

  const [review, setReview] = useState<ICreateReview>({ message: '', mark: 0 });
  const handleMark = (event: unknown, value: unknown) => {
    setReview({ message: review.message, mark: value as number });
  };
  const handleMessage = (event: React.ChangeEvent<{ value: unknown }>) => {
    const value = event.target.value as string;
    setReview({ message: value, mark: review.mark });
  };

  const submit = () => {
    // sent request, wait for result
    props.addReview({ message: review.message, mark: review.mark, username: 'me' });
    // if there is user's comment -> redraw form (new values) and button label = 'update'
  };

  return (
    <Card variant="outlined" className={classes.root}>
      <Grid item xs={12} container direction="column" alignItems="center" justify="center">
        <Grid className={classes.cardPart} container direction="row" alignItems="center" justify="flex-end">
          <Rating value={review.mark} onChange={handleMark} />
        </Grid>
        <TextField
          id="outlined-multiline-static"
          label="Review"
          multiline
          rows={4}
          placeholder="Write your review here"
          variant="outlined"
          value={review.message}
          onChange={handleMessage}
          fullWidth
        />
        <Grid className={classes.cardPart} container alignItems="center" justify="flex-end">
          <Button color="primary" variant="contained" onClick={submit}>
            Отправить
          </Button>
        </Grid>
      </Grid>
    </Card>
  );
};

export default ReviewForm;
