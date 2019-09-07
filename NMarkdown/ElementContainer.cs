using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Text;

namespace NMarkdown
{
    public abstract class ElementContainer: 
        Element, IElementContainer
    {
        protected ElementContainer()
        {
            (SubElements as ObservableCollection<Element>).CollectionChanged += ElementContainer_CollectionChanged;
        }

        private void ElementContainer_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (var element in e.NewItems)
                {
                    (element as Element).Parent = this;
                    (element as Element).Document = Document;
                }
            }
        }

        public IList<Element> SubElements { get; } = new ObservableCollection<Element>();

        public override void SerializeTo(StringBuilder markdown)
        {
            foreach (var element in SubElements)
            {
                element.SerializeTo(markdown);
            }
        }
    }
}
