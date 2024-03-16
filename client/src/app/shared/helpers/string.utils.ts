export const trimPostCommentReaction = (value: string): string => {
  if (value.includes('@')) {
    const parts = value.split(' ');
    return parts.slice(1).join(' ').trim();
  } else {
    return value.trim(); // Return original input if no @username is present
  }
};

export const shouldRemoveMetion = (value: string, keyCode: string): boolean => {
  const regex = /@\w+\s?/;
  const match = value.match(regex);
  if (keyCode === 'Backspace' && match && match[0] === value) {
    return true;
  }
  return false;
};
