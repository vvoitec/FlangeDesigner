using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using FlangeDesigner.AbstractEngine;
using FlangeDesigner.AbstractEngine.Exceptions;
using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;
using Dimension = FlangeDesigner.AbstractEngine.Dimension;

namespace FlangeDesigner.SolidWorksEngine
{
    public class Model : IModel
    {
        private readonly IEngine _engine;
        private ModelDoc2 _swDoc;
        private int _fileerror;
        private int _filewarning;
        public string Name { get; }
        
        public ICollection<IEnumerable<Dimension>> ProjectConfigurations { get; }
        
        public Model(ModelDoc2 swDoc)
        {
            _swDoc = swDoc;
            Name = _swDoc.GetTitle();

            var designTable = (DesignTable)swDoc.GetDesignTable();
            designTable.Attach();
            
            var nRows = designTable.GetTotalRowCount();
            var nColumns = designTable.GetTotalColumnCount();

            // collect headers
            var headers = new List<string>();

            for (int column = 1; column < nColumns + 1; column++)
            {
                // row = 0 - header row
                var cellText = designTable.GetEntryText(0, column);
                headers.Add(cellText);
            }

            ProjectConfigurations = new List<IEnumerable<Dimension>>();
            
            // collect data
            for (int row = 1; row < nRows + 1; row++)
            {
                var modelConfiguration = new ModelConfiguration();
                
                for (int column = 1; column < nColumns + 1; column++)
                {
                    var cellText = designTable.GetEntryText(row, column);
                    var dimension = new Dimension(
                            headers[column - 1],
                            Length.of(Int32.Parse(cellText))
                        );
                    
                    modelConfiguration.Add(dimension);
                }
                
                ProjectConfigurations.Add(modelConfiguration);
                
            }

            designTable.Detach();
        }

        public void AddConfiguration(IEnumerable<Dimension> modelConfiguration)
        {
            var designTable = (DesignTable) _swDoc.GetDesignTable();

            string[] cells = modelConfiguration.Select(dimension => Convert.ToString(dimension.Value)).ToArray();
            cells = new[] {"dupa"}.Concat(cells).ToArray(); // first element as empty string
            designTable.Attach();
            designTable.Warn = true;
            designTable.EditTable2(true);
            var err = designTable.AddRow(cells);
            err = designTable.UpdateTable((int)swDesignTableUpdateOptions_e.swUpdateDesignTableAll, true);
            // if (err)
            // {
            //     throw new ModelException("Failed to add configuration to design table: " + (swDesignTableErrors_e) designTable.LastError);
            // }
            designTable.Detach();

            ProjectConfigurations.Add(modelConfiguration);
        }
    }
}
