class Chip {
  final String label;
  final String icon;
  final void Function() onPressed;

  Chip({
    required this.label,
    required this.icon,
    required this.onPressed,
  });
}
