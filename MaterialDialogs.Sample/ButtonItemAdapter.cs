using System;
using Android.Content;
using Android.Views;
using Android.Widget;
using Android.Support.V7.Widget;

namespace MaterialDialogs.Sample
{
    class ButtonItemAdapter : RecyclerView.Adapter
    {
        string[] _items;

        internal Action<int> ItemAction { get; private set; }
        internal Action<int> ButtonAction { get; private set; }

        public ButtonItemAdapter(Context context, int arrayResId)
            : this(context.Resources.GetTextArray(arrayResId))
        {
        }

        private ButtonItemAdapter(string[] items)
        {
            _items = items;
        }

        public void SetCallbacks(Action<int> itemAction, Action<int> buttonAction)
        {
            ItemAction = itemAction;
            ButtonAction = buttonAction;
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View view = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.dialog_customlistitem, parent, false);
            return new ButtonVH(view, this);
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            var viewHolder = holder as ButtonVH;
            viewHolder.Title.Text = $"{_items[position]} ({position})";
            viewHolder.Button.Tag = position;
        }

        public override int ItemCount => _items.Length;

        class ButtonVH : RecyclerView.ViewHolder, View.IOnClickListener
        {
            public TextView Title { get; private set; }
            public Button Button { get; private set; }

            ButtonItemAdapter _adapter;

            public ButtonVH(View itemView, ButtonItemAdapter adapter)
                : base(itemView)
            {
                Title = itemView.FindViewById<TextView>(Resource.Id.md_title);
                Button = itemView.FindViewById<Button>(Resource.Id.md_button);

                _adapter = adapter;

                itemView.SetOnClickListener(this);
                Button.SetOnClickListener(this);
            }

            public void OnClick(View view)
            {
                if (view is Button)
                {
                    _adapter?.ButtonAction?.Invoke(AdapterPosition);
                }
                else
                {
                    _adapter?.ItemAction?.Invoke(AdapterPosition);
                }
            }
        }
    }
}